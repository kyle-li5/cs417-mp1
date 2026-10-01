using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class FuelTank : MonoBehaviour {
    public GameObject fuel_tank_top_panel;
    public Transform fuel_tank_panel_open_ref;
    public Transform fuel_tank_panel_close_ref;
    public float anim_duration;
    private IEnumerator AnimateFuelTankDoorOpen() {
    	float duration = Mathf.Max(anim_duration, 0.001f);
        
        Vector3 s = fuel_tank_panel_close_ref.position;
        Vector3 e = fuel_tank_panel_open_ref.position;
        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            t = t * t * (3f - 2f * t);

            fuel_tank_top_panel.transform.position = Vector3.Lerp(s, e, t);

            yield return null;
        }

        fuel_tank_top_panel.transform.position = e;
    }
    public void PlayFuelTankPanelOpen() { StartCoroutine(AnimateFuelTankDoorOpen()); }

    private IEnumerator AnimateFuelTankDoorClose()
    {
        float duration = Mathf.Max(anim_duration, 0.001f);

        Vector3 s = fuel_tank_panel_open_ref.position;
        Vector3 e = fuel_tank_panel_close_ref.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            t = t * t * (3f - 2f * t);

            fuel_tank_top_panel.transform.position = Vector3.Lerp(s, e, t);

            yield return null;
        }

        fuel_tank_top_panel.transform.position = e;
    }
    public void PlayFuelTankPanelClose() { StartCoroutine(AnimateFuelTankDoorClose()); }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        FuelReceiver.fuelFull += PlayFuelTankPanelClose;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
