using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
/*
The plot: Once "powered" by the solved circuit, the breaker box's main indicator light turns red from the default gray (meaning "off"). Then the player must insert all breakers into the box's slots; this results in the main indicator turning to green. Then the user must toggle breakers in specific slots to a specific state (on/off) to "direct" power to certain "spaceship" components as indicated on each slot's corresponding label. Once all breakers have been inserted and set in the right combination, "power" to the exit door is restored 
*/
public class BreakerBox : MonoBehaviour {
    private const int BREAKER_ROWS = 3;
    private const int BREAKER_COLS = 2;
    private bool[,] solution_config;
    private int num_breakers_placed;
    private bool fuse_placed;
    private bool door_open;
    private bool anim_in_progress;
    [SerializeField] private RoomController controller;
    // Must have all breakers inserted before functional
    private bool functional;
    private Quaternion closed_door_rotation;
    private Breaker?[,] breakers; 
    public GameObject[] breaker_slots;
    public GameObject door_pivot;
    public GameObject door_handle;
    public GameObject[] slot_indicators;
    public Material indicator_off_mat;
    public Material indicator_true_mat;
    public Material indicator_false_mat;
    public GameObject main_indicator;
    public float door_open_anim_duration;
    public float door_close_anim_duration;
    public float door_handle_anim_duration;
    public float handle_door_anim_delay;
    

    private IEnumerator AnimateDoorHandleTurn(float duration) {
        if (door_open) { yield break; }
        
        duration = Mathf.Max(duration, 0.001f);
        float half_duration = duration / 2f;

        Quaternion start_handle_rotation = door_handle.transform.localRotation;
        Quaternion end_handle_rotation = start_handle_rotation * Quaternion.Euler(0f, 55f, 0f);
        float elapsed = 0f;

        // initial turn
        while (elapsed < half_duration) {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / half_duration);

            t = t * t * (3f - 2f * t);

            door_handle.transform.localRotation = Quaternion.Lerp(start_handle_rotation, end_handle_rotation, t);
            yield return null;
        }

        // Guarantees same end rot
        door_handle.transform.localRotation = end_handle_rotation;

        // Turn back
        elapsed = 0f;

        while (elapsed < half_duration) {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / half_duration);

            t = t * t * (3f - 2f * t);
            door_handle.transform.localRotation = Quaternion.Lerp(end_handle_rotation, start_handle_rotation, t);

            yield return null;
        }

        // restore to original rotation
        door_handle.transform.localRotation = start_handle_rotation;
        
    }

    private IEnumerator AnimateDoorOpen(float duration) {
        if (door_open) { yield break; }
        
        duration = Mathf.Max(duration, 0.001f);
        
        Quaternion start_rotation = door_pivot.transform.localRotation;
        Quaternion end_rotation = start_rotation * Quaternion.Euler(0f, 0f, 145f);
        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            t = t * t * (3f - 2f * t);

            door_pivot.transform.localRotation = Quaternion.Lerp(start_rotation, end_rotation, t);
            yield return null;
        }

        door_pivot.transform.localRotation = end_rotation;
        door_open = true;
    }

    private IEnumerator AnimateDoorClose(float duration) {
        if (!door_open || anim_in_progress) { yield break; }
        anim_in_progress = true;
        duration = Mathf.Max(duration, 0.001f);
        
        Quaternion start_rotation = door_pivot.transform.localRotation;
        Quaternion end_rotation = closed_door_rotation;
        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            t = t * t * (3f - 2f * t);

            door_pivot.transform.localRotation = Quaternion.Lerp(start_rotation, end_rotation, t);
            yield return null;
        }

        // Guarantees same end rot
        door_pivot.transform.localRotation = end_rotation;
        door_open = false;
        anim_in_progress = false;
    }

    private IEnumerator AnimateDoorOpenSequence(float door_duration, float handle_duration, float handle_door_delay) {
        if (anim_in_progress) { yield break; }
        anim_in_progress = true;
        StartCoroutine(AnimateDoorHandleTurn(handle_duration));
        yield return new WaitForSeconds(handle_door_delay);
        yield return StartCoroutine(AnimateDoorOpen(door_duration));
        anim_in_progress = false;
    }
    private void PlayDoorClosed() { StartCoroutine(AnimateDoorClose(door_close_anim_duration)); }
    private void PlayDoorOpenSequence() { StartCoroutine(AnimateDoorOpenSequence(door_open_anim_duration, door_handle_anim_duration, handle_door_anim_delay)); }
    
    // Lock box to current state
    public void DisableFunctionality() {
        if (!functional) { return; }
        for (int i = 0; i < BREAKER_ROWS; i++) {
            for (int j = 0; j < BREAKER_COLS; j++) {
                breakers[i, j].toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(breakers[i, j].OnToggleSelected);
                
                breakers[i, j].toggle_interactable.interactionLayers = InteractionLayerMask.GetMask("Nothing");
            }
        }
        breakers[0, 0].toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(OnBreaker1StateChange);
        breakers[1, 0].toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(OnBreaker2StateChange);
        breakers[2, 0].toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(OnBreaker3StateChange);
        breakers[0, 1].toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(OnBreaker4StateChange);
        breakers[1, 1].toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(OnBreaker5StateChange);
        breakers[2, 1].toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(OnBreaker6StateChange);
        functional = false;
    }
    private bool PuzzleStateEval() {
        if (num_breakers_placed != BREAKER_ROWS * BREAKER_COLS) { return false; }

        for (int i = 0; i < BREAKER_ROWS; i++) {
            for (int j = 0; j < BREAKER_COLS; j++) {
                // Shouldn't happen at this point
                if (breakers[i, j] == null) { return false; }

                if (breakers[i, j].state != solution_config[i, j]) { return false; }
            }
        }

        return true;
    } 
    private void FlipIndicator(int n) {
        (int, int) p = (n % BREAKER_ROWS, n / BREAKER_ROWS);
    
        slot_indicators[n].GetComponent<MeshRenderer>().material = breakers[p.Item1, p.Item2].state ? indicator_true_mat : indicator_false_mat;

        if (PuzzleStateEval()) {
            if (!functional) { return; }
            DisableFunctionality();
            if (controller != null) { controller.ReportOnBreakerPuzzleSolved(); }
        }
    }
    public void OnBreaker1StateChange(SelectEnterEventArgs args) { /*Debug.Log();*/ FlipIndicator(0); }
    public void OnBreaker2StateChange(SelectEnterEventArgs args) { /*Debug.Log();*/ FlipIndicator(1); }
    public void OnBreaker3StateChange(SelectEnterEventArgs args) { /*Debug.Log();*/ FlipIndicator(2); }
    public void OnBreaker4StateChange(SelectEnterEventArgs args) { /*Debug.Log();*/ FlipIndicator(3); }
    public void OnBreaker5StateChange(SelectEnterEventArgs args) { /*Debug.Log();*/ FlipIndicator(4); }
    public void OnBreaker6StateChange(SelectEnterEventArgs args) { /*Debug.Log();*/ FlipIndicator(5); }

    private Breaker InsertHelper(SelectEnterEventArgs args, int n) {
        (int, int) p = (n % BREAKER_ROWS, n / BREAKER_ROWS);

        Transform t = args.interactableObject.transform;
        XRGrabInteractable grab = t.GetComponent<XRGrabInteractable>();
        grab.interactionLayers = InteractionLayerMask.GetMask("Installed");

        Breaker b = t.GetComponent<Breaker>();
        breakers[p.Item1, p.Item2] = b;
        b.toggle_interactable.interactionLayers = InteractionLayerMask.GetMask("Breaker");
        b.toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(b.OnToggleSelected);
        
        slot_indicators[n].GetComponent<MeshRenderer>().material = b.state ? indicator_true_mat : indicator_false_mat;
        
        num_breakers_placed++;

        return b;
    }
    public void SetMainIndicatorTrue() {
        main_indicator.GetComponent<MeshRenderer>().material = indicator_true_mat;
    }
    public void SetMainIndicatorFalse() {
        main_indicator.GetComponent<MeshRenderer>().material = indicator_false_mat;
    }
    public void SetMainIndicatorOff() {
        main_indicator.GetComponent<MeshRenderer>().material = indicator_off_mat;
    }
    public void OnFuseInsert(SelectEnterEventArgs args) {
        Transform t = args.interactableObject.transform;
        XRGrabInteractable grab = t.GetComponent<XRGrabInteractable>();
        fuse_placed = true;
        grab.interactionLayers = InteractionLayerMask.GetMask("Installed");
        
        if (num_breakers_placed == BREAKER_ROWS * BREAKER_COLS) {
            SetMainIndicatorTrue();
            functional = true;
            if (PuzzleStateEval()) {
                DisableFunctionality();
                if (controller != null) { controller.ReportOnBreakerPuzzleSolved(); }
            }
        }
        
    }
    public void OnSlot1BreakerInsert(SelectEnterEventArgs args) {
        Breaker b = InsertHelper(args, 0);
        b.toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnBreaker1StateChange);
        Validate();
    }
    public void OnSlot2BreakerInsert(SelectEnterEventArgs args) {
        Breaker b = InsertHelper(args, 1);
        b.toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnBreaker2StateChange);
        Validate();
    }
    public void OnSlot3BreakerInsert(SelectEnterEventArgs args) {
        Breaker b = InsertHelper(args, 2);
        b.toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnBreaker3StateChange);
        Validate();
    }
    public void OnSlot4BreakerInsert(SelectEnterEventArgs args) {
        Breaker b = InsertHelper(args, 3);
        b.toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnBreaker4StateChange);
        Validate();
    }
    public void OnSlot5BreakerInsert(SelectEnterEventArgs args) {
        Breaker b = InsertHelper(args, 4);
        b.toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnBreaker5StateChange);
        Validate();
    }
    public void OnSlot6BreakerInsert(SelectEnterEventArgs args) {
        Breaker b = InsertHelper(args, 5);
        b.toggle_interactable.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(OnBreaker6StateChange);
        Validate();
    }
    public void Validate() {
        if (num_breakers_placed != BREAKER_ROWS * BREAKER_COLS) { return; }
        if (!fuse_placed) { return; }
        functional = true;
        SetMainIndicatorTrue();
    }
    // public void OnValid() {

    // }
    public void OnDoorHandleSelect(SelectEnterEventArgs args) {
        if (anim_in_progress) { return; }
        if (!door_open) {
            PlayDoorOpenSequence();
        } else {
            PlayDoorClosed();
        }
    }
    void Start() {
        closed_door_rotation = door_pivot.transform.localRotation;
        num_breakers_placed = 0;
        fuse_placed = false;
        solution_config = new bool[,]{
            {false, true},
            {true, false},
            {true, true}
        };
        main_indicator.GetComponent<MeshRenderer>().material = indicator_off_mat;
        anim_in_progress = false;
        breakers = new Breaker?[BREAKER_ROWS, BREAKER_COLS];
        for (int i = 0; i < BREAKER_ROWS; i++) {
            for (int j = 0; j < BREAKER_COLS; j++) {
                breakers[i, j] = null;
            }
        }
        for (int i = 0; i < slot_indicators.Length; i++) {
            MeshRenderer r = slot_indicators[i].GetComponent<MeshRenderer>();
            r.material = indicator_off_mat;
        }

    }

    // Update is called once per frame
    void Update() {
        
    }
}
