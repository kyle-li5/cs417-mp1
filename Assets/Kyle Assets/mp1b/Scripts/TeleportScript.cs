using UnityEngine;


public class TeleportScript : MonoBehaviour
{
    [SerializeField] private UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationAnchor previousAnchor;
    
    public void PlayerTeleported(UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationAnchor newAnchor) {
        if (previousAnchor != null && previousAnchor != newAnchor) {
            HandlePreviousAnchor(previousAnchor);
        }
        previousAnchor = newAnchor;
        newAnchor.gameObject.GetComponent<MeshRenderer>().enabled = false;
        newAnchor.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    public void HandlePreviousAnchor(UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation.TeleportationAnchor newAnchor) {
        newAnchor.gameObject.GetComponent<MeshRenderer>().enabled = true;
        newAnchor.gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
