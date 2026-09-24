using UnityEngine;
using System.Collections;

public class FuelDispenserScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Material redGlow;
    [SerializeField] Material greenGlow;
    [SerializeField] MeshRenderer meshRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HologramScript.correctSOS += HandleFuel;
    }

    private void OnDestroy() {
        HologramScript.correctSOS -= HandleFuel;
    }

    void HandleFuel(bool fuel) {
        StartCoroutine(PlayAnim());
    }

    IEnumerator PlayAnim() {
        Material[] materials = meshRenderer.materials;
        materials[0] = greenGlow;
        meshRenderer.materials = materials;
        yield return new WaitForSeconds(0.5f);
        animator.Play("FuelDispenserAnimation");
    }
}
