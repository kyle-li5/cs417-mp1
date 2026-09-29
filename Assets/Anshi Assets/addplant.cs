using UnityEngine;

public class addplant : MonoBehaviour
{
    public GameObject potSoil;
    public GameObject x;
    public AudioClip sound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        risefall r = FindAnyObjectByType<risefall>();
    }
    void OnTriggerEnter(Collider s){
        print("hi");
        if (s.CompareTag("seed")){
            GameObject a = Instantiate(potSoil, transform.position, transform.rotation);
            Instantiate(x, new Vector3(7.25f,8.7f,1.5f),Quaternion.Euler(45f,0f,0f));
            Destroy(s.gameObject);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(sound,new Vector3(5.415f, 3.69f,-.169f));
            risefall r = FindAnyObjectByType<risefall>();
            r.move(a);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
