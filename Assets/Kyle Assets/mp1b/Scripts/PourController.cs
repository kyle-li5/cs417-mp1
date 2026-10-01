using UnityEngine;

public class PourController : MonoBehaviour
{
    [SerializeField] ParticleSystem fuelStream;
    [SerializeField] float minPourAngle = 50f;
    [SerializeField] float maxPourAngle = 135f;
    [SerializeField] float minPourRate = 10f;
    [SerializeField] float maxPourRate = 150f;

    [SerializeField] bool enablePour;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enablePour = false;

        FuelReceiver.fuelFull += HandleControlPanel;
        FuelProgressBar.fullyFueled += HandleFuelingStation;
    }
    void HandleControlPanel()
    {
        fuelStream.Stop();
        enablePour = false;
    }

    void HandleFuelingStation(bool b)
    {
        enablePour = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enablePour)
        {
            float tiltAngle = Vector3.Angle(transform.up, Vector3.up);

            if (tiltAngle > minPourAngle)
            {
                if (!fuelStream.isPlaying)
                {
                    fuelStream.Play();
                }
                float pourRate = Mathf.InverseLerp(minPourAngle, maxPourAngle, tiltAngle);
                var emission = fuelStream.emission;
                emission.rateOverTime = Mathf.Lerp(minPourRate, maxPourRate, pourRate);
            }
            else
            {
                if (fuelStream.isPlaying)
                {
                    fuelStream.Stop();
                }
            }
        }
    }
}
