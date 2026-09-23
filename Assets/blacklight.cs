using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class blacklight : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject box;
    public InputActionReference action;
    public TMP_Text t;
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx)=>
        {        
            Destroy(box);
            t.text = "Clues Left:\nSoil: 0\nSeed: 1\nWater: 1";
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    
    }
}
