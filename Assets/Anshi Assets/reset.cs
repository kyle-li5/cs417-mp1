using UnityEngine.InputSystem;
using UnityEngine;
using TMPro;
public class reset : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public InputActionReference action;
    public TMP_Text t;
    public waterfilling s;
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx)=>
        {
            
            GameObject[] drops = GameObject.FindGameObjectsWithTag("drop");
            foreach (GameObject drop in drops){
                Destroy(drop);
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
