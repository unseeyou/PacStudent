using System.Collections;
using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject Countdown;
    private int _score = 0;

    public void UpdateScore(int score)
    {
        _score += score;
        scoreText.text = $"{_score:D6}";
    }
    
    private IEnumerator RunCountdown()
    {
        if (Countdown != null) Countdown.SetActive(true);
        TextMeshProUGUI countdownText = Countdown.GetComponentInChildren<TextMeshProUGUI>();
        
        if (countdownText != null) countdownText.gameObject.SetActive(true);
        
        string[] countdownSequence = { "3", "2", "1", "GO!" };
        
        foreach (string step in countdownSequence)
        {
            if (countdownText != null)
            {
                countdownText.SetText(step);
            }
            
            yield return new WaitForSecondsRealtime(1);
        }
        
        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        if (Countdown != null)
        {
            Countdown.SetActive(false);
        }
    }

    private void Start()
    {
        StartCoroutine(RunCountdown());
    }
}
