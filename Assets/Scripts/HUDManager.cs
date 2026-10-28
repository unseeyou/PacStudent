using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int _score = 0;

    public void UpdateScore(int score)
    {
        _score += score;
        scoreText.text = $"{_score:D6}";
    }
}
