using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Breaker : MonoBehaviour {
    internal bool state;
    //bool installed;
    private Vector3 true_rotation = new Vector3(0, -45, 0);
    private Vector3 false_rotation = new Vector3(0, 45, 0);

    public GameObject handle_stem;
    public XRSimpleInteractable toggle_interactable;


    public void ToggleState() {
        
        handle_stem.transform.localRotation = Quaternion.Euler(state ? false_rotation : true_rotation);
        state = !state;
        
    }

    public void OnToggleSelected(SelectEnterEventArgs args) {
        ToggleState();
    }

    void Start() {
        state = false;
        //installed = false;
        handle_stem.transform.localRotation = Quaternion.Euler(state ? true_rotation : false_rotation);
    }
}