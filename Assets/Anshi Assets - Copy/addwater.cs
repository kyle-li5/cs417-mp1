using UnityEngine;
public class addwater : MonoBehaviour
{
    public GameObject potSoil;
    public GameObject x;
    public AudioClip sound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        end sc = FindAnyObjectByType<end>();
        risefall r = FindAnyObjectByType<risefall>();
    }
    void OnTriggerEnter(Collider s){
        print("hi2");
        if (s.CompareTag("water")){
            GameObject a = Instantiate(potSoil, transform.position, transform.rotation);
            Instantiate(x, new Vector3(-3.78107f,5.067f,-14.613f),Quaternion.Euler(45f,0f,0f));
            Destroy(s.gameObject);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(sound,new Vector3(5.415f, 3.69f,-.169f));
            end sc = FindAnyObjectByType<end>();
            sc.Finish();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
