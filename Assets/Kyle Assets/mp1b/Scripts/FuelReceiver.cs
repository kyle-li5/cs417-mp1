using System;
using UnityEngine;
using UnityEngine.UI;

public class FuelReceiver : MonoBehaviour
{
    [SerializeField] float currentFuel = 0f;
    [SerializeField] float maxFuel = 100f;
    [SerializeField] float fillPerParticle = 0.1f;
    [SerializeField] Transform fuelPivot;
    [SerializeField] float maxHeight = 0.4f;
    [SerializeField] Image fuelFill;

    public static event Action fuelFull;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fuelPivot.localScale = new Vector3(0f, fuelPivot.localScale.y, fuelPivot.localScale.z);
        fuelFill.fillAmount = 0.02f;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (currentFuel < maxFuel)
        {
            currentFuel += fillPerParticle;
            currentFuel = Mathf.Min(currentFuel, maxFuel);
            UpdateVisualLevel();
        }
        else if (currentFuel == maxFuel)
        {
            fuelFull?.Invoke();
        }
    }
    
    void UpdateVisualLevel()
    {
        float fillPercentage = currentFuel / maxFuel;
        Vector3 newScale = fuelPivot.localScale;
        newScale.x = fillPercentage * maxHeight;
        fuelPivot.localScale = newScale;
        fuelFill.fillAmount = fillPercentage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
