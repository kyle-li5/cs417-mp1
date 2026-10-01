using System.Collections;
using UnityEngine;

public class pingsound : MonoBehaviour
{
    [SerializeField] AudioSource[] roomOneAudio;
    [SerializeField] AudioSource[] roomTwoAudio;
    [SerializeField] AudioSource[] roomThreeAudio;
    [SerializeField] float pingCooldown = 30f;

    [SerializeField] int room;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        room = 0;
        StartCoroutine(PlaySounds());
        RoomController.finishedRoomOne += HandleRoomOne;
        end.finishedRoomTwo += HandleRoomTwo;
        FuelProgressBar.fullyFueled += HandleRoomThree;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void HandleRoomOne()
    {
        room = 1;
    }

    void HandleRoomTwo()
    {
        room = 2;
    }

    void HandleRoomThree(bool b)
    {
        if (b)
        {
            room = 3;
        }
    }

    IEnumerator PlaySounds() {
        while(true) {
            if (room == 0)
            {
                foreach (AudioSource audio in roomOneAudio)
                {
                    audio.Play();
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(pingCooldown);
            }
            else if (room == 1)
            {
                foreach (AudioSource audio in roomTwoAudio)
                {
                    audio.Play();
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(pingCooldown);
            }
            else if (room == 2)
            {
                foreach (AudioSource audio in roomThreeAudio)
                {
                    audio.Play();
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(pingCooldown);
            }
            else if (room >= 3)
            {
                break;
            }
        }
    }
}
