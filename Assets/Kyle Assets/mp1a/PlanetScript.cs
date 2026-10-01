using UnityEngine;

public class PlanetScript : MonoBehaviour
{
    private Transform transform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 10*Time.deltaTime, 0);
    }
}
