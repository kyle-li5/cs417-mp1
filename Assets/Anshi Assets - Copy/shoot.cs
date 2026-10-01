using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class shoot : MonoBehaviour
{
    public InputActionReference action;
    public GameObject obj;
    public Transform c;
    public ParticleSystem par;
    public AudioClip sound;
    public AudioSource audioSource;
    public Vector3 pla = new Vector3(5,12,-3);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        action.action.Enable();
        action.action.performed += (ctx)=>
        {
            par.Play();
            audioSource.PlayOneShot(sound);
            GameObject o = Instantiate(obj,c.position,c.rotation);
            arbit v = o.GetComponent<arbit>();
            Vector3 d= o.transform.position - pla;
            double distance = Math.Sqrt(Math.Pow(d.x,2)+Math.Pow(d.y,2)+Math.Pow(d.z,2));
            d.Normalize();
            Vector3 vd = c.forward-Vector3.Dot(c.forward,d)*d;
            double speed = Math.Sqrt(15/distance);
            
            v.velocity = vd*(float)speed;
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
