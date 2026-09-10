using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] Transform leftControllerTransform;
    [SerializeField] Vector3 insideTransform;
    [SerializeField] Vector3 outsideTransform;
    private bool inside;

    [SerializeField] GameObject spawnPrefab;
    [SerializeField] GameObject facePrefab;
    [SerializeField] GameObject angryFacePrefab;
    private bool spawned;
    private GameObject face;

    [SerializeField] AudioClip destroyClip;

    public InputActionReference breakOutAction;
    public InputActionReference spawnAction;
    public InputActionReference faceAction;
    public InputActionReference angryFaceAction;

    void Start()
    {
        // break out logic
        //insideTransform = new Vector3(0, 2.80173f, 0);
        //outsideTransform = new Vector3(0, 6.1f, -28.53f);
        transform.position = insideTransform;
        inside = true;

        breakOutAction.action.Enable();
        breakOutAction.action.performed += (ctx) =>
        {
            if (inside)
            {
                transform.position = outsideTransform;
                inside = false;
            } else
            {
                transform.position = insideTransform;
                inside = true;
            }
        };

        // spawning logic
        spawnAction.action.Enable();
        spawnAction.action.performed += (ctx) =>
        {
            //if (!spawned)
            //{
            //    //GameObject face = Instantiate(spawnPrefab, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
            //    //face = Instantiate(spawnPrefab, new Vector3(0, 8.91f, 0), Quaternion.identity);
            //    face = Instantiate(spawnPrefab, new Vector3(2, 10, 5), Quaternion.identity);
            //    face.GetComponent<FaceScript>().setVelocity(leftControllerTransform.forward); 
            //    face.transform.GetChild(0).GetComponent<AudioSource>().Play();
            //    spawned = true;
            //} else
            //{
            //    AudioSource.PlayClipAtPoint(destroyClip, face.transform.position);
            //    Destroy(face);
            //    spawned = false;
            //}
            GameObject obj = Instantiate(spawnPrefab, new Vector3(0, 12, 0), Quaternion.identity);
            obj.GetComponent<SpawnedPlanetScript>().setVelocity(leftControllerTransform.forward);
            // face.transform.GetChild(0).GetComponent<AudioSource>().Play();
            // spawned = true;
        };

        // face logic
        faceAction.action.Enable();
        faceAction.action.performed += (ctx) =>
        {
            Transform camera = transform.GetChild(0).transform.GetChild(0).transform;
            Instantiate(facePrefab, new Vector3(camera.position.x, camera.position.y, 7), Quaternion.identity);
        };

        // angry face logic
        spawned = false;
        angryFaceAction.action.Enable();
        angryFaceAction.action.performed += (ctx) =>
        {
            if (!spawned)
            {
               face = Instantiate(angryFacePrefab, new Vector3(0, 8.91f, 0), Quaternion.identity);
            //    face.transform.GetChild(0).GetComponent<AudioSource>().Play();
               spawned = true;
            } else
            {
               AudioSource.PlayClipAtPoint(destroyClip, face.transform.position);
               Destroy(face);
               spawned = false;
            }
        };
    }
    
    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            if (inside)
            {
                transform.position = outsideTransform;
                inside = false;
            }
            else
            {
                transform.position = insideTransform;
                inside = true;
            }
        }
    }
}
