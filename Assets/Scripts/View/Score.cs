using TMPro;

public class Score
{
    private readonly TextMeshProUGUI scoreText;
    private readonly D2048 d2048;

    public Score(TextMeshProUGUI scoreText, D2048 d2048)
    {
        this.scoreText = scoreText;
        this.d2048 = d2048;
    }

    public void UpdateScore()
    {
        if (scoreText)
        {
            scoreText.text = "Score: " + d2048.Score.ToString();
        }
    }
}
