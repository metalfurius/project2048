using TMPro;
using UnityEngine;

public class Score
{
    private TextMeshProUGUI scoreText;
    private D2048 d2048;

    public Score(TextMeshProUGUI scoreText, D2048 d2048)
    {
        this.scoreText = scoreText;
        this.d2048 = d2048;
    }

    public void UpdateScore()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + d2048.score.ToString();
        }
    }
}
