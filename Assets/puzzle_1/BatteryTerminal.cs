using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BatteryTerminal : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void OnBatteryPlaced(SelectEnterEventArgs args) {
        XRGrabInteractable battery = args.interactableObject.transform.GetComponent<XRGrabInteractable>();

        if (battery != null){
            //Debug.Log("BatteryTerminal: Battery installed");
            battery.interactionLayers = InteractionLayerMask.GetMask("Installed");
        }
    }
    
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
