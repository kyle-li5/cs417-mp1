using UnityEngine;
using TMPro;
public class end : MonoBehaviour
{
    public TMP_Text t;
    public ParticleSystem par;
    public AudioClip sound;
    public Transform location;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void Finish(){
        print("hi3");
        t.text = "Oxygen Restored! You Win!";
        t.color = Color.green;
        par.Play(true);
        AudioSource.PlayClipAtPoint(sound, location.position);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
