using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public GameObject aster;
    public InputActionReference action;
    public Transform location;
    public AudioClip sound;
    public ParticleSystem par;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx)=>
        {
            AudioSource.PlayClipAtPoint(sound, location.position);
            Instantiate(aster, location.position, location.rotation);
            par.Play(true);
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
