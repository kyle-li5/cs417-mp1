using UnityEngine;
using System;

public class ScoreboardScript : MonoBehaviour
{
    [SerializeField] GameObject[] lockLights;
    [SerializeField] Material redGlow;
    [SerializeField] Material yellowGlow;
    [SerializeField] Material greenGlow;
    
    [SerializeField] bool[] completedLocks;

    [SerializeField] Animator door;
    [SerializeField] GameObject exitTeleporter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        exitTeleporter.SetActive(false);
        completedLocks = new bool[4];
        PowerCableScript.pluggedIn += HandlePlug;
        CollectibleScript.keycard += HandleKeycard;
        HologramScript.correctSOS += HandleFuel;
        FuelProgressBar.fullyFueled += HandleFuel;
    }

    private void OnDestroy() {
        PowerCableScript.pluggedIn -= HandlePlug;
        CollectibleScript.keycard -= HandleKeycard;
        HologramScript.correctSOS -= HandleFuel;
        FuelProgressBar.fullyFueled -= HandleFuel;
    }

    void HandlePlug(bool isPluggedIn) {
        MeshRenderer meshRenderer = lockLights[0].GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;
        materials[0] = greenGlow;
        meshRenderer.materials = materials;
        completedLocks[0] = true;
    }

    public void HandleKeycard(bool keycard) {
        MeshRenderer meshRenderer = lockLights[1].GetComponent<MeshRenderer>();
        if (!keycard) {
            Material[] materials = meshRenderer.materials;
            materials[0] = yellowGlow;
            meshRenderer.materials = materials;
        } else {
            Material[] materials = meshRenderer.materials;
            materials[0] = greenGlow;
            meshRenderer.materials = materials;
            completedLocks[1] = true;
            HandleUSB(false);
        }
    }

    public void HandleUSB(bool usb) {
        MeshRenderer meshRenderer = lockLights[2].GetComponent<MeshRenderer>();
        if (!usb) {
            Material[] materials = meshRenderer.materials;
            materials[0] = yellowGlow;
            meshRenderer.materials = materials;
        } else {
            Material[] materials = meshRenderer.materials;
            materials[0] = greenGlow;
            meshRenderer.materials = materials;
            completedLocks[2] = true;
        }
    }

    public void HandleFuel(bool fuel) {
        MeshRenderer meshRenderer = lockLights[3].GetComponent<MeshRenderer>();
        if (!fuel) {
            Material[] materials = meshRenderer.materials;
            materials[0] = yellowGlow;
            meshRenderer.materials = materials;
        } else {
            Material[] materials = meshRenderer.materials;
            materials[0] = greenGlow;
            meshRenderer.materials = materials;
            completedLocks[3] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (completedLocks[0] == true && completedLocks[1] == true && completedLocks[2] == true && completedLocks[3] == true) {
            door.Play("ExitDoorOpen");
            exitTeleporter.SetActive(true);
            this.enabled = false;
        }
    }
}
