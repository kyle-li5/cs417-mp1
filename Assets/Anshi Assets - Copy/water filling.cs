using UnityEngine;
using TMPro;

public class waterfilling : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int total;
    public TMP_Text t;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    void Start()
    {
        total = 0;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider s){
        print("hi2");
        if (s.CompareTag("drop")){
            print("col");
            total+=1;
            t.text = total + "/10";
             Destroy(s.gameObject);
        if (total == 10) {
            t.text = "Water Level = 10!!";
            grabInteractable.enabled = true;
        }
        
        }
       
    }
    
}
