using System.Collections;
using UnityEngine;

public class pingsound : MonoBehaviour
{
    [SerializeField] AudioSource[] audioSources;
    [SerializeField] float pingCooldown = 30f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(PlaySounds());   
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator PlaySounds() {
        while(true) {
            foreach (AudioSource audio in audioSources) {
                audio.Play();
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(pingCooldown);
        }
    }
}
