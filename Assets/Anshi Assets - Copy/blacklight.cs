using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class blacklight : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject box;
    public InputActionReference action;
    public TMP_Text t;
    private void OnEnable()
    {
        action.action.Enable();
        action.action.performed += OnBlacklightAction; 
    }

    private void OnDisable()
    {
        action.action.performed -= OnBlacklightAction; 
        action.action.Disable(); 
    }

    private void OnBlacklightAction(InputAction.CallbackContext ctx)
    {
        Destroy(box);
        t.text = "Clues Left:\nSoil: 0\nSeed: 1\nWater: 1";
    }
}
