using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Vector3 insideTransform;
    [SerializeField] Vector3 outsideTransform;
    private bool inside;

    public InputActionReference breakOutAction;
    public InputActionReference spawnAction;

    void Start()
    {
        // break out logic
        //insideTransform = new Vector3(0, 2.80173f, 0);
        //outsideTransform = new Vector3(0, 6.1f, -28.53f);
        transform.position = insideTransform;
        inside = true;

        breakOutAction.action.Enable();
        breakOutAction.action.performed += (ctx) =>
        {
            if (inside)
            {
                transform.position = outsideTransform;
                inside = false;
            } else
            {
                transform.position = insideTransform;
                inside = true;
            }
        };

        // spawning logic
    }
    
    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (inside)
            {
                transform.position = outsideTransform;
                inside = false;
            }
            else
            {
                transform.position = insideTransform;
                inside = true;
            }
        }
    }
}
