using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class PlantContainer : MonoBehaviour
{
    public GameObject plant_container_top_panel;
    public Transform plant_container_panel_open_ref;
    public Transform plant_container_panel_close_ref;
    public float anim_duration;
    private IEnumerator AnimatePlantContainerDoorOpen()
    {
        float duration = Mathf.Max(anim_duration, 0.001f);

        Vector3 s = plant_container_panel_close_ref.position;
        Vector3 e = plant_container_panel_open_ref.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            t = t * t * (3f - 2f * t);

            plant_container_top_panel.transform.position = Vector3.Lerp(s, e, t);

            yield return null;
        }

        plant_container_top_panel.transform.position = e;
    }
    public void PlayPlantContainerPanelOpen() { StartCoroutine(AnimatePlantContainerDoorOpen()); }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
