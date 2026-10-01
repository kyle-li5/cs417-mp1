using UnityEngine;
using UnityEngine.InputSystem;

public class breakout : MonoBehaviour
{
    public InputActionReference action;

    public Transform og;
    public Transform newr;

    public ParticleSystem par;
    public ParticleSystem par2;

    public AudioClip sound;
    public AudioSource audioSource;
    public AudioClip sound2;
    public AudioSource audioSource2;

    public bool t = false;

    void Start()
    {
        //used GPT to debug sound playing
        transform.position = og.position;
        action.action.Enable();
        action.action.performed += (ctx) =>
        {
            if (!t)
            {
                par.Play();
                audioSource.PlayOneShot(sound);

                transform.position = newr.position;

                t = true;
            }
            else
            {
                par2.Play();
                audioSource2.PlayOneShot(sound2);

                transform.position = og.position;

                t = false;
            }
        };
    }
}