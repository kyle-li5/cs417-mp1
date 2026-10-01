using System;
using UnityEngine;

public class PowerCableScript : MonoBehaviour
{
    public static event Action<bool> pluggedIn;
    public static event Action<bool> unplugged;

    public void PlugIn() {
        pluggedIn?.Invoke(true);
    }

    public void Unplug() {
        unplugged?.Invoke(true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
