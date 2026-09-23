using UnityEngine;
using UnityEngine.InputSystem;
public class LightSwitch : MonoBehaviour
{
    public Light light;
    public InputActionReference action;
    public ParticleSystem par;
    public AudioClip sound;
    public AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Color og= light.color;
        light = GetComponent<Light>();
        action.action.Enable();
        
        action.action.performed += (ctx)=>
        {
            par.Play();
            Color currentColor = light.color;
            audioSource.PlayOneShot(sound);
            if (currentColor == og)
            {
                light.color = new Color(0.3f, 0.4f, 0.6f);
            }
            else
            {
                light.color = og;
            }
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
