using UnityEngine;

public class FuelingStationScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject greenLight;
    [SerializeField] GameObject redLight;
    [SerializeField] Material greenGlow;
    [SerializeField] Material redGlow;
    [SerializeField] Material blackLED;

    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] GameObject cube;

    bool gateOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // meshRenderer.materials[0].SetColor("_BaseColor", new Color(0.62428f, 1, 0.57647f, 0));
        // cube.SetActive(false);
        gateOn = false;
        PowerCableScript.unplugged += HandleUnplug;
        PowerCableScript.pluggedIn += HandlePlug;
    }

    void onDestroy() {
        PowerCableScript.unplugged -= HandleUnplug;
        PowerCableScript.pluggedIn -= HandlePlug;
    }

    void HandleUnplug(bool unplug) {
        if (gateOn) {
            MeshRenderer greenMesh = greenLight.GetComponent<MeshRenderer>();
            Material[] materials = greenMesh.materials;
            materials[0] = blackLED;
            greenMesh.materials = materials;

            MeshRenderer redMesh = redLight.GetComponent<MeshRenderer>();
            materials = redMesh.materials;
            materials[0] = redGlow;
            redMesh.materials = materials;

            animator.Play("FuelBlockerAnimation");
            gateOn = false;
        }
    }

    void HandlePlug(bool pluggedIn) {
        if (!gateOn) {
            MeshRenderer greenMesh = greenLight.GetComponent<MeshRenderer>();
            Material[] materials = greenMesh.materials;
            materials[0] = greenGlow;
            greenMesh.materials = materials;

            MeshRenderer redMesh = redLight.GetComponent<MeshRenderer>();
            materials = redMesh.materials;
            materials[0] = blackLED;
            redMesh.materials = materials;

            animator.Play("FuelReblockAnim");
            gateOn = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
