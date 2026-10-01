using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class releaseseed : MonoBehaviour
{
    public GameObject s;
    public InputActionReference action;
    public TMP_Text t;
    private void OnEnable()
    {
        action.action.Enable();
        action.action.performed += OnReleaseSeedAction;
    }

    private void OnDisable()
    {
        action.action.performed -= OnReleaseSeedAction;
        action.action.Disable();
    }
    
    private void OnReleaseSeedAction(InputAction.CallbackContext ctx)
    {
        Instantiate(s, new Vector3(0f, 7f, -16.1f), transform.rotation);
        t.text = "Clues Left:\nSoil: 0\nSeed: 0\nWater: 1";
    }
}
