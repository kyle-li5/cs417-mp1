using System;
using UnityEngine;

public class FaceScript : MonoBehaviour
{
    private Vector3 velocity;
    const double gravity = 50;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //velocity = new Vector3(2f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position - new Vector3(0, 7.5f, 0);
        double distance = Math.Sqrt(Math.Pow(position.x, 2) + Math.Pow(position.y, 2) + Math.Pow(position.z, 2));
        double ax = -gravity * position.x / Math.Pow(distance, 3);
        double ay = -gravity * position.y / Math.Pow(distance, 3);
        double az = -gravity * position.z / Math.Pow(distance, 3);

        velocity.x = velocity.x + (float)ax * Time.deltaTime;
        velocity.y = velocity.y + (float)ay * Time.deltaTime;
        velocity.z = velocity.z + (float)az * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
    }

    public void setVelocity(Vector3 v) { velocity = 2*v; }
}
