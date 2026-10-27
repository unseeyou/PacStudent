using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TextMeshProUGUI timerText;
    private float _timeMilliseconds;
    private int _timeMinutes;
    private int _timeSeconds;
    
    void Start()
    {
        timerText.SetText("00:00:00");
        _timeMilliseconds = 0;
        _timeMinutes = 0;
        _timeSeconds = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timerText.SetText($"{_timeMinutes:D2}:{_timeSeconds:D2}:{_timeMilliseconds.ToString("00").Replace(".", "").Substring(0, 2)}");
        _timeMilliseconds += Time.deltaTime * 1000f;
        if (_timeMilliseconds >= 1000)
        {
            _timeMilliseconds -= 1000;
            _timeSeconds += 1;
        }
        if (_timeSeconds >= 60)
        {
            _timeMinutes += 1;
            _timeSeconds -= 60; 
        }
    }
}
