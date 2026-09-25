using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonScript : MonoBehaviour
{
    public Transform buttonCap;
    public float pressDepth = 0.129f;
    
    private Vector3 startPosition;

    void Start()
    {
        startPosition = buttonCap.localPosition;
        
        var interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();
        interactable.activated.AddListener(OnPressed);
        interactable.deactivated.AddListener(OnReleased);
    }

    void OnPressed(ActivateEventArgs  args)
    {
        buttonCap.localPosition = startPosition + new Vector3(0, -pressDepth, 0);
    }

    void OnReleased(DeactivateEventArgs  args)
    {
        buttonCap.localPosition = startPosition;
    }
}
