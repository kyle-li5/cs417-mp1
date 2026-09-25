using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class HologramScript : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Material blackLED;
    [SerializeField] Material redGlow;
    [SerializeField] Material yellowGlow;
    [SerializeField] Material greenGlow;

    [SerializeField] GameObject redHolo;
    [SerializeField] GameObject greenHolo;

    [SerializeField] GameObject submitButton;
    [SerializeField] Color submitGreen = new Color(129, 250, 83);
    [SerializeField] Color submitRed = new Color(255, 0, 0);
    [SerializeField] MorseScript morseScript;
    [SerializeField] GameObject finishedText;

    public static event Action<bool> correctSOS;

    bool pluggedIn;

    void Start()
    {
        finishedText.SetActive(false);
        pluggedIn = false;
        Material[] materials = meshRenderer.materials;
        materials[0] = blackLED;
        meshRenderer.materials = materials;
        redHolo.SetActive(false);
        greenHolo.SetActive(false);

        PowerCableScript.pluggedIn += HandleMessage;
        PowerCableScript.unplugged += HandleUnplug;
    }

    private void HandleMessage(bool isPluggedIn) {
        pluggedIn = isPluggedIn;
        Debug.Log("plugged in");
        if (isPluggedIn) {
            Material[] materials = meshRenderer.materials;
            materials[0] = redGlow;
            meshRenderer.materials = materials;
            redHolo.SetActive(true);
        }
    }

    private void HandleUnplug(bool isPluggedIn) {
        Material[] materials = meshRenderer.materials;
        materials[0] = blackLED;
        meshRenderer.materials = materials;
        redHolo.SetActive(false);
        greenHolo.SetActive(false);
    }

    private void OnDestroy() {
        PowerCableScript.pluggedIn -= HandleMessage;
        PowerCableScript.unplugged -= HandleUnplug;
    }

    public void Activate() {
        if (pluggedIn) {
            Material[] materials = meshRenderer.materials;
            materials[0] = yellowGlow;
            meshRenderer.materials = materials;
            redHolo.SetActive(false);
            greenHolo.SetActive(true);
        }
    }

    public void ValidateAnswer() {
        bool correct = morseScript.CheckGrid();
        if (correct) {
            greenHolo.SetActive(false);
            Material[] materials = meshRenderer.materials;
            materials[0] = greenGlow;
            meshRenderer.materials = materials;
            correctSOS?.Invoke(false);
            finishedText.SetActive(true);
        }
        else {
            StartCoroutine(WrongAnswer());
        }
    }

    IEnumerator WrongAnswer() {
        Material[] materials = meshRenderer.materials;
        materials[0] = redGlow;
        meshRenderer.materials = materials;
        submitButton.GetComponent<Image>().color = submitRed;
        yield return new WaitForSeconds(2f);

        materials = meshRenderer.materials;
        materials[0] = yellowGlow;
        meshRenderer.materials = materials;
        submitButton.GetComponent<Image>().color = submitGreen;
        morseScript.ClearGrid();
    }
}
