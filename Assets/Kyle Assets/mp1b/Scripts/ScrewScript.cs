using UnityEngine;
using System;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class ScrewScript : MonoBehaviour
{
    [SerializeField] Material whiteGlow;
    private XRGrabInteractable grabInteractable;
    public static event Action<bool> collected;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.activated.AddListener(OnTriggerPulled);
    }

    private void OnDisable()
    {
        grabInteractable.activated.RemoveListener(OnTriggerPulled);
    }

    private void OnTriggerPulled(ActivateEventArgs args)
    {
        HandleCollect();
    }

    private void HandleCollect() {
        collected?.Invoke(true);
        StartCoroutine(DestroySelf());
    }
    
    IEnumerator DestroySelf() {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material[] materials = meshRenderer.materials;
        materials[0] = whiteGlow;
        meshRenderer.materials = materials;
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
}
