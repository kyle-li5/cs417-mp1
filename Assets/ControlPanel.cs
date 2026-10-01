using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ControlPanel : MonoBehaviour {
    public Transform start;
    public Transform end;
    public float anim_duration;
    public GameObject control_panel;

    private IEnumerator AnimateControlPanel() {
        float duration = Mathf.Max(anim_duration, 0.001f);
        
        Vector3 s = start.position;
        Vector3 e = end.position;
        float elapsed = 0f;

        while (elapsed < duration) {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            t = t * t * (3f - 2f * t);

            control_panel.transform.position = Vector3.Lerp(s, e, t);

            yield return null;
        }

        control_panel.transform.position = e;
    }
    public void PlayControlPanelAnim() { StartCoroutine(AnimateControlPanel());}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }
}
