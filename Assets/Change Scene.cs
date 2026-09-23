using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public InputActionReference action;
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx)=>
        {
            SceneManager.LoadScene("SampleScene");
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
