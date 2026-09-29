using UnityEngine;
using System;
public class cometscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Vector3 position;
    Vector3 velocity;
    double ax;
    double ay;
    double az;
    const double gravity = 1.5;
    
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
        double distance = Math.Sqrt( Math.Pow(position.x, 2) + Math.Pow (position.y-7.5, 2) + Math.Pow(position.z, 2) );
        ax = - gravity * position.x / Math.Pow(distance, 3);
        ay = - gravity * (position.y-7.5) / Math.Pow(distance, 3);
        az = - gravity * position.z / Math.Pow(distance, 3);
        velocity.x = velocity.x + (float)ax * Time.deltaTime;
        velocity.y = velocity.y + (float)ay * Time.deltaTime;
        velocity.z = velocity.z + (float)az * Time.deltaTime;
        position.x= position.x + velocity.x*Time.deltaTime;
        position.y= position.y + velocity.y*Time.deltaTime;
        position.z= position.z + velocity.z*Time.deltaTime;
        transform.position = position;
    }
}
