using UnityEngine;
using System;
public class arbit : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector3 position;
    public Vector3 velocity;
    double ax;
    double ay;
    double az;
    const double gravity = 20.5;
    
    void Start()
    {
        position = transform.position;
        velocity.x = 0; 
        velocity.y = 0; 
        velocity.z = 1;
    }
    
    // Update is called once per frame
    void Update()
    {
        position=transform.position;
        double distance = Math.Sqrt( Math.Pow(position.x-5, 2) + Math.Pow (position.y-12, 2) + Math.Pow(position.z+3, 2) );
        ax = - gravity * (position.x-5) / Math.Pow(distance, 3);
        ay = - gravity * (position.y-12) / Math.Pow(distance, 3);
        az = - gravity * (position.z+3) / Math.Pow(distance, 3);
        velocity.x = velocity.x + (float)ax * Time.deltaTime;
        velocity.y = velocity.y + (float)ay * Time.deltaTime;
        velocity.z = velocity.z + (float)az * Time.deltaTime;
        position.x= position.x + velocity.x*Time.deltaTime;
        position.y= position.y + velocity.y*Time.deltaTime;
        position.z= position.z + velocity.z*Time.deltaTime;
        transform.position = position;
    }
}
