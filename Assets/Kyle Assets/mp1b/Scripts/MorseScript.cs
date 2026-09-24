using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MorseScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private TextMeshProUGUI[] morseChars;
    [SerializeField] private float dashThreshold;

    private float pressStartTime;
    private int currentIndex = 0;

    void Start()
    {
        ClearGrid();
    }

    public void OnPointerDown(PointerEventData eventData) {
        pressStartTime = Time.time;
    }

    public void OnPointerUp(PointerEventData eventData) {
        if (currentIndex >= morseChars.Length) {
            return;
        }
        float duration = Time.time - pressStartTime;

        if (duration < dashThreshold) {
            morseChars[currentIndex].text = "•";
        }
        else {
            morseChars[currentIndex].text = "–";
        }
        currentIndex++;
    }

    public void ClearGrid() {
        currentIndex = 0;
        for (int i = 0; i < morseChars.Length; ++i) {
            morseChars[i].text = "";
        }
    }

    public bool CheckGrid() {
        string[] correctChars = {"•", "•", "•", "–", "–", "–", "•", "•", "•"};
        if (morseChars.Length < correctChars.Length) {
            return false;
        }
        for (int i = 0; i < morseChars.Length; ++i) {
            if (morseChars[i].text != correctChars[i]) {
                return false;
            }
        }
        return true;
    }
}
