using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public enum RoomState: int {
    Initial = 0,
    BatteryPlaced = 1,
    CircuitSolved = 2,
    BreakerBoxSolved = 3,
    PowerRestored = 4
}

public class RoomController : MonoBehaviour {

    private RoomState current_state = RoomState.Initial;
    public Light room_lighting;
    public Light player_headlamp;
    public GameObject[] terminal_pipes;
    public GameObject[] circuit_pipes;
    public GameObject[] breaker_pipes;
    public Material powered_mat;
    public GameObject door_panel;
    public GameObject door_panel_anim_to;
    [SerializeField] private CircuitPuzzle circuit_puzzle;
    [SerializeField] private BreakerBox breaker_box;

    public static event Action finishedRoomOne;
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
    private IEnumerator AnimateExitDoor(GameObject door_panel, GameObject to, float duration) {
        duration = Mathf.Max(duration, 0.001f);
        
        Vector3 start = door_panel.transform.position;
        Vector3 end = door_panel_anim_to.transform.position;
        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            t = t * t * (3f - 2f * t);

            door_panel.transform.position = Vector3.Lerp(start, end, t);

            yield return null;
        }

        door_panel.transform.position = end;
        finishedRoomOne?.Invoke();
    }
    public void ReportOnBatteryPlaced(SelectEnterEventArgs args) {
        Debug.Log("RoomController: Battery installed");
        for (int i = 0; i < terminal_pipes.Length; i++) {
            if (terminal_pipes[i] != null) {
                MeshRenderer r = terminal_pipes[i].GetComponent<MeshRenderer>();
                Material current = terminal_pipes[i].GetComponent<MeshRenderer>().material;
                StartCoroutine(AnimateIndicator(r, powered_mat, 2f));
            }
        }
        circuit_puzzle.PuzzleEnable(3);

    }
    public void ReportOnCircuitPuzzleSolved() {
        Debug.Log("RoomController: Circuit puzzle solved");
        for (int i = 0; i < circuit_pipes.Length; i++) {
            if (circuit_pipes[i] != null) {
                MeshRenderer r = circuit_pipes[i].GetComponent<MeshRenderer>();
                Material current = circuit_pipes[i].GetComponent<MeshRenderer>().material;
                StartCoroutine(AnimateIndicator(r, powered_mat, 2f));
            }
        }
        breaker_box.SetMainIndicatorFalse();

    }
    public void ReportOnBreakerPuzzleSolved() {
        Debug.Log("RoomController: Breaker puzzle solved");
        for (int i = 0; i < breaker_pipes.Length; i++) {
            if (breaker_pipes[i] != null) {
                MeshRenderer r = breaker_pipes[i].GetComponent<MeshRenderer>();
                Material current = breaker_pipes[i].GetComponent<MeshRenderer>().material;
                StartCoroutine(AnimateIndicator(r, powered_mat, 2f));
            }
        }
        StartCoroutine(AnimateExitDoor(door_panel,door_panel_anim_to, 4));
    }

}
