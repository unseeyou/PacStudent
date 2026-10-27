using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI timerText;
    
    void Start()
    {
        timerText.SetText("00:00:00");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
