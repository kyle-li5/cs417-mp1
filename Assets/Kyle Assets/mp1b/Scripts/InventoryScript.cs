using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] GameObject inventoryUI;
    [SerializeField] Transform leftHand;
    [SerializeField] Vector3 positionOffset = new Vector3(0f, -0.2f, 0.1f);
    public InputActionReference inventoryAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //inventoryUI.SetActive(false);
        inventoryUI.transform.localScale = Vector3.zero;
        inventoryAction.action.Enable();
        inventoryAction.action.performed += ToggleInventory;
        Debug.Log("script started");
    }

    private void OnDestroy()
    {
        inventoryAction.action.performed -= ToggleInventory;
    }

    private void ToggleInventory(InputAction.CallbackContext context)
    {
        bool isActive = inventoryUI.transform.localScale.x > 0.01f;
        Debug.Log(isActive);

        if (!isActive)
        {
            //inventoryUI.SetActive(true);
            inventoryUI.transform.localScale = Vector3.one;
            inventoryUI.transform.position = leftHand.position + leftHand.TransformDirection(positionOffset);
            if (Camera.main != null)
            {
                inventoryUI.transform.LookAt(Camera.main.transform.position);
                inventoryUI.transform.Rotate(0, 180, 0);
            }
        }
        else
        {
            //inventoryUI.SetActive(false);
            inventoryUI.transform.localScale = Vector3.zero;
            inventoryUI.transform.position = new Vector3(0, -1000f, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
