using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CollectibleScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textScore;
    private int score;
    [SerializeField] int maxScore;
    [SerializeField] GameObject boxTop;
    [SerializeField] GameObject boxFront;

    public static event Action<bool> keycard;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        textScore.text = score.ToString();
        ScrewScript.collected += HandleMessage;
    }

    private void OnDestroy() {
        ScrewScript.collected -= HandleMessage;
    }

    private void HandleMessage(bool collected) { 
        score += 1;
        textScore.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (score == maxScore) {
            boxTop.SetActive(false);
            boxFront.SetActive(false);
            keycard.Invoke(false);
            enabled = false;
        }
    }
}
