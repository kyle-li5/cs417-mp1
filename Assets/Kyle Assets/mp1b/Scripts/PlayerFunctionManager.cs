using UnityEngine;

public class PlayerFunctionManager : MonoBehaviour
{
    water waterScript;
    blacklight blacklight;
    releaseseed releaseSeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterScript = GetComponent<water>();
        blacklight = GetComponent<blacklight>();
        releaseSeed = GetComponent<releaseseed>();
        DisableScripts();

        RoomController.finishedRoomOne += HandleRoomTwo;
        end.finishedRoomTwo += HandleRoomThree;
    }

    void EnableScripts() {
        waterScript.enabled = true;
        blacklight.enabled = true;
        releaseSeed.enabled = true;
    }
    void DisableScripts() {
        waterScript.enabled = false;
        blacklight.enabled = false;
        releaseSeed.enabled = false;
    }

    void HandleRoomTwo()
    {
        EnableScripts();
    }

    void HandleRoomThree()
    {
        DisableScripts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
