using UnityEngine;

public class ObjectVelocity : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Vector3 velocity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity*Time.deltaTime;
    }
}
