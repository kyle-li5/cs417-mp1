using UnityEngine;
using UnityEngine.InputSystem;

public class LightSwitchScript : MonoBehaviour
{
    [SerializeField] Light currentLight;

    public InputActionReference action;
    void Start()
    {
        currentLight = GetComponent<Light>();
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            //currentLight.color = Color.red;
            currentLight.color = new Color(Random.value, Random.value, Random.value);
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.tabKey.wasPressedThisFrame)
        {
            currentLight.color = new Color(Random.value, Random.value, Random.value);
        }
    }
}
