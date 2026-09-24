using UnityEngine;

public class IDScanner : MonoBehaviour
{
    [SerializeField] GameObject light;
    [SerializeField] Material greenLight;
    [SerializeField] Material redLight;

    [SerializeField] GameObject door;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light.GetComponent<Renderer>().material = redLight;
    }

    public void GreenLight()
    {
        light.GetComponent<Renderer>().material = greenLight;
    }
}
