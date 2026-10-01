using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ControlPanel : MonoBehaviour {
    public Transform start;
    public Transform end;
    public float anim_duration;
    public GameObject control_panel;

    public Transform fuelStart;
    public Transform fuelEnd;
    public GameObject fuelCanvas;

    // canvas stuff
    public GameObject noFuelText;
    public GameObject launchText;
    public GameObject launchButton;

    // warpspeed stuff
    public Transform warpStart;
    public Transform warpEnd;
    public GameObject warpspeed;
    public GameObject plane1;
    public GameObject plane2;
    public GameObject plane3;
    public ParticleSystem particleSystem;

    private IEnumerator AnimateControlPanel() {
        float duration = Mathf.Max(anim_duration, 0.001f);
        
        Vector3 s = start.position;
        Vector3 e = end.position;

        Vector3 s_fuel = fuelStart.position;
        Vector3 e_fuel = fuelEnd.position;
        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            t = t * t * (3f - 2f * t);

            control_panel.transform.position = Vector3.Lerp(s, e, t);

            fuelCanvas.transform.position = Vector3.Lerp(s_fuel, e_fuel, t);

            yield return null;
        }

        control_panel.transform.position = e;
        fuelCanvas.transform.position = e_fuel;
    }
    public void PlayControlPanelAnim() { StartCoroutine(AnimateControlPanel());}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        noFuelText.SetActive(true);
        launchText.SetActive(false);
        launchButton.SetActive(false);
        FuelReceiver.fuelFull += HandleFuelReceiver;

        //plane1.SetActive(false);
        //plane2.SetActive(false);
        //plane3.SetActive(false);
        warpspeed.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        
    }
    void HandleFuelReceiver()
    {
        noFuelText.SetActive(false);
        launchText.SetActive(true);
        launchButton.SetActive(true);
    }

    public void Launch()
    {
        warpspeed.SetActive(true);
        StartCoroutine(LaunchAnim());
    }

    private IEnumerator LaunchAnim()
    {
        particleSystem.Play();
        yield return new WaitForSeconds(1f);

        float duration = Mathf.Max(0.5f, 0.001f);

        Vector3 s = warpStart.position;
        Vector3 e = warpEnd.position;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            t = t * t * (3f - 2f * t);

            warpspeed.transform.position = Vector3.Lerp(s, e, t);

            yield return null;
        }

        warpspeed.transform.position = e;
        //plane1.SetActive(true);
        //plane2.SetActive(true);
        //plane3.SetActive(true);
    }
}
