using UnityEngine.InputSystem;
using UnityEngine;
public class ExitButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public InputActionReference action;
    public AudioClip sound;
    public AudioSource audioSource;
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx)=>
        {
            audioSource.PlayOneShot(sound);
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying=false;
            #else 
                Application.Quit();
            #endif
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
