using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class releaseseed : MonoBehaviour
{
    public GameObject s;
    public InputActionReference action;
    public TMP_Text t;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx)=>
        {
            
            Instantiate(s,new Vector3(0f,7f,0f), transform.rotation);
            t.text = "Clues Left:\nSoil: 0\nSeed: 0\nWater: 1";
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
