using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class FuelProgressBar : MonoBehaviour
{
    [SerializeField] Material yellowOpaque;
    [SerializeField] Material redOpaque;
    [SerializeField] Material greenOpaque;

    [SerializeField] MeshRenderer fuelButton;
    [SerializeField] Image progressFill;
    [SerializeField] float fillSpeed = 0.2f;

    bool fuel;
    private Coroutine fillCoroutine;

    public static event Action<bool> fullyFueled;

    void Start() {
        progressFill.fillAmount = 0;
        fuel = false;

        Material[] materials = fuelButton.materials;
        materials[0] = redOpaque;
        fuelButton.materials = materials;
    }

    public void FuelIn() {
        fuel = true;
        if (progressFill.fillAmount >= 1) {
            Material[] materials = fuelButton.materials;
            materials[0] = greenOpaque;
            fuelButton.materials = materials;
        } else {
            Material[] materials = fuelButton.materials;
            materials[0] = yellowOpaque;
            fuelButton.materials = materials;
        }
    }

    public void FuelOut() {
        fuel = false;
        Material[] materials = fuelButton.materials;
        materials[0] = redOpaque;
        fuelButton.materials = materials;

        if (fillCoroutine != null) {
            StopCoroutine(fillCoroutine);
            fillCoroutine = null;
        }
    }

    public void StartFill() {
        if (fuel && fillCoroutine == null && progressFill.fillAmount < 1){ 
            fillCoroutine = StartCoroutine(fillFuel());
        }
    }

    IEnumerator fillFuel() {
        while (progressFill.fillAmount < 1) {
            progressFill.fillAmount += fillSpeed * Time.deltaTime;
            yield return null;
        }
        progressFill.color = new Color32(97, 255, 87, 255);
        Material[] materials = fuelButton.materials;
        materials[0] = greenOpaque;
        fuelButton.materials = materials;
        fullyFueled?.Invoke(true);

        fillCoroutine = null;
    }
}
