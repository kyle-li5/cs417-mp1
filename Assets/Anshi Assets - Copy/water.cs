using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class water : MonoBehaviour
{
    public GameObject droplets;
    public InputActionReference action;
    public TMP_Text t;

    private void OnEnable()
    {
        action.action.Enable();
        action.action.performed += SpawnWater; 
    }

    private void OnDisable()
    {
        action.action.performed -= SpawnWater; 
        action.action.Disable(); 
    }

    private void SpawnWater(InputAction.CallbackContext ctx)
    {
        t.text = "Clues Left:\nSoil: 0\nSeed: 0\nWater: 0";
        for (int i = 0; i < 10; i += 1) 
        {
            Vector3 offset = new Vector3(Random.Range(-6f, 6f), 6f, Random.Range(-7f, 7f));
            Instantiate(droplets, transform.position + offset, Quaternion.identity);
        }
    }
}
