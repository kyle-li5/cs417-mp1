using UnityEngine;
using TMPro;
using System;
public class end : MonoBehaviour
{
    public TMP_Text t;
    public ParticleSystem par;
    public AudioClip sound;
    public Transform location;
    public GameObject wall;

    public Animator entranceDoorAnimator;

    public static event Action finishedRoomTwo;
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
        Destroy(wall);
        entranceDoorAnimator.Play("OpenEntranceDoor");
        finishedRoomTwo?.Invoke();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
