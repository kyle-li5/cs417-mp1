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
            Instantiate(x, new Vector3(7.25f,8.7f,-1f),Quaternion.Euler(45f,0f,0f));
            Destroy(s.gameObject);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(sound,new Vector3(5.415f, 3.69f,-.169f));
            risefall r = FindAnyObjectByType<risefall>();
            r.move(a);
            end sc = FindAnyObjectByType<end>();
            sc.Finish();
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
