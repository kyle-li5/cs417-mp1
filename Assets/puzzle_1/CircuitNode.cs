using UnityEngine;

public class CircuitNode : MonoBehaviour {
   
    public enum Side: int {
        Up,
        Down,
        Left,
        Right
    }
    public enum LightState {
        Red,
        Green,
        Off
    }


    internal bool up;
    internal bool down;
    internal bool left;
    internal bool right;
    public GameObject up_indicator;
    public GameObject down_indicator;
    public GameObject left_indicator;
    public GameObject right_indicator;
    public Material green_mat;
    public Material red_mat;
    public Material off_mat;
    private LightState up_state;
    private LightState down_state;
    private LightState left_state;
    private LightState right_state;

    public void SetLight(Side s, LightState ls) {
        switch (s) {
            case Side.Up: {
                switch (ls) {
                    case LightState.Green: { up_indicator.GetComponent<MeshRenderer>().material = green_mat; up = true; up_state = LightState.Green; break; }
                    case LightState.Red: { up_indicator.GetComponent<MeshRenderer>().material = red_mat; up = false; up_state = LightState.Red; break; }
                    case LightState.Off: { up_indicator.GetComponent<MeshRenderer>().material = off_mat; up = false; up_state = LightState.Off; break; }
                }
                break;
            }
            case Side.Down: {
                switch (ls) {
                    case LightState.Green: { down_indicator.GetComponent<MeshRenderer>().material = green_mat; down = true; down_state = LightState.Green; break; }
                    case LightState.Red: { down_indicator.GetComponent<MeshRenderer>().material = red_mat; down = false; down_state = LightState.Red; break; }
                    case LightState.Off: { down_indicator.GetComponent<MeshRenderer>().material = off_mat; down = false; down_state = LightState.Off; break; }
                }
                break;
            }
            case Side.Left: {
                switch (ls) {
                    case LightState.Green: { left_indicator.GetComponent<MeshRenderer>().material = green_mat; left = true; left_state = LightState.Green; break; }
                    case LightState.Red: { left_indicator.GetComponent<MeshRenderer>().material = red_mat; left = false; left_state = LightState.Red; break; }
                    case LightState.Off: { left_indicator.GetComponent<MeshRenderer>().material = off_mat; left = false; left_state = LightState.Off; break; }
                }
                break;
            }
            case Side.Right: {
                switch (ls) {
                    case LightState.Green: { right_indicator.GetComponent<MeshRenderer>().material = green_mat; right = true; right_state = LightState.Green; break; }
                    case LightState.Red: { right_indicator.GetComponent<MeshRenderer>().material = red_mat; right = false; right_state = LightState.Red; break; }
                    case LightState.Off: { right_indicator.GetComponent<MeshRenderer>().material = off_mat; right = false; right_state = LightState.Off; break; }
                }
                break;
            }
            // default: { return; }
        }
    }
    public LightState GetLight(Side s) {
        switch (s) {
            case Side.Up: { return up_state; }
            case Side.Down: { return down_state; }
            case Side.Left: { return left_state; }
            case Side.Right: { return right_state; }
            // should never happen, but makes compiler happy
            default: { return LightState.Off; }
        }
    }

    void Awake() {
        SetLight(Side.Up, LightState.Off);
        SetLight(Side.Down, LightState.Off);
        SetLight(Side.Left, LightState.Off);
        SetLight(Side.Right, LightState.Off);
    }
}
