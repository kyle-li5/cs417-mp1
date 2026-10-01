using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using System.Collections;
using System.Collections.Generic;


public class ControlRoom : MonoBehaviour {
    private int batteries_placed;
    public Material powered_mat;
    public GameObject[] terminal_pipes_g1;
    public GameObject[] terminal_pipes_g2;
    public GameObject[] terminal_pipes_g3;
    public GameObject[] terminal_pipes_g4;
    public GameObject[] terminal_pipes_g5;
    public GameObject[] terminal_pipes_g6;
    public GameObject[] terminal_pipes_g7;
    public GameObject[] terminal_pipes_g1_2;
    public GameObject[] terminal_pipes_g2_2;
    public GameObject[] terminal_pipes_g3_2;
    public GameObject[] terminal_pipes_g4_2;
    public float pipe_anim_duration;
    public float sequence_anim_delay;
    public ControlPanel control_panel_ref;
    public FuelTank fuel_tank;
    public PlantContainer plant_container;

    private IEnumerator AnimateIndicator(MeshRenderer renderer, Material end, float duration) {
        duration = Mathf.Max(duration, 0.001f);
        Material mat = renderer.material;
        Color start_color = mat.GetColor("_Color");
        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            t = t * t * (3f - 2f * t);

            mat.color = Color.Lerp(start_color, end.GetColor("_Color"), t);

            yield return null;
        }

        mat.color = end.GetColor("_Color");
    }

    private IEnumerator AnimatePipeSequence(float delay) {
        StartCoroutine(AnimateIndicator(terminal_pipes_g1[0].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
        StartCoroutine(AnimateIndicator(terminal_pipes_g1[1].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));

        StartCoroutine(AnimateIndicator(terminal_pipes_g1_2[0].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
        StartCoroutine(AnimateIndicator(terminal_pipes_g1_2[1].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));

        yield return new WaitForSeconds(delay);

        StartCoroutine(AnimateIndicator(terminal_pipes_g2[0].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
        StartCoroutine(AnimateIndicator(terminal_pipes_g2[1].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));

        StartCoroutine(AnimateIndicator(terminal_pipes_g2_2[0].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
        StartCoroutine(AnimateIndicator(terminal_pipes_g2_2[1].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));

        yield return new WaitForSeconds(delay);

        StartCoroutine(AnimateIndicator(terminal_pipes_g3[0].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
        StartCoroutine(AnimateIndicator(terminal_pipes_g3[1].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));

        StartCoroutine(AnimateIndicator(terminal_pipes_g3_2[0].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
        StartCoroutine(AnimateIndicator(terminal_pipes_g3_2[1].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));

        yield return new WaitForSeconds(delay);

        StartCoroutine(AnimateIndicator(terminal_pipes_g4[0].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
        StartCoroutine(AnimateIndicator(terminal_pipes_g4[1].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));

        StartCoroutine(AnimateIndicator(terminal_pipes_g4_2[0].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
        StartCoroutine(AnimateIndicator(terminal_pipes_g4_2[1].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));

        yield return new WaitForSeconds(delay);
        StartCoroutine(AnimateIndicator(terminal_pipes_g5[0].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
        StartCoroutine(AnimateIndicator(terminal_pipes_g5[1].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
	yield return new WaitForSeconds(delay);
	StartCoroutine(AnimateIndicator(terminal_pipes_g6[0].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
        StartCoroutine(AnimateIndicator(terminal_pipes_g6[1].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
	yield return new WaitForSeconds(delay);
	StartCoroutine(AnimateIndicator(terminal_pipes_g7[0].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
        StartCoroutine(AnimateIndicator(terminal_pipes_g7[1].GetComponent<MeshRenderer>(), powered_mat, pipe_anim_duration));
        OnPipeSequenceComplete();
    }
    
    private void PlayPipePowerSequence(float delay) { StartCoroutine(AnimatePipeSequence(delay)); }
    public void OnControlRoomBatteryPlaced(SelectEnterEventArgs args) {
        batteries_placed++;
        XRGrabInteractable battery = args.interactableObject.transform.GetComponent<XRGrabInteractable>();
        if (battery != null) { battery.interactionLayers = InteractionLayerMask.GetMask("Installed"); }
        if (batteries_placed != 2) { return; }
        PlayPipePowerSequence(sequence_anim_delay);
    }
    private void OnPipeSequenceComplete() {
        control_panel_ref.PlayControlPanelAnim();
        fuel_tank.PlayFuelTankPanelOpen();
        plant_container.PlayPlantContainerPanelOpen();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        batteries_placed = 0;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
