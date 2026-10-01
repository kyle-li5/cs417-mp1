using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IDScanner : MonoBehaviour
{
    [SerializeField] GameObject light;
    [SerializeField] Material holoBlack;
    [SerializeField] Material greenLight;
    [SerializeField] Material redLight;

    [SerializeField] GameObject socket;
    [SerializeField] GameObject door;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light.GetComponent<Renderer>().material = holoBlack;
        PowerCableScript.pluggedIn += HandleMessage;
        socket.SetActive(false);
    }

    private void OnDestroy() {
        PowerCableScript.pluggedIn -= HandleMessage;
    }

    public void RedLight() {
        light.GetComponent<Renderer>().material = redLight;
        socket.SetActive(true);
    }

    public void GreenLight()
    {
        light.GetComponent<Renderer>().material = greenLight;
    }

    private void HandleMessage(bool isPluggedIn) {
        Debug.Log("plugged in");
        if (isPluggedIn) {
            RedLight();
        }
    }
}
