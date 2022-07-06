using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    public int Score { get; private set; }

    [SerializeField]
    public int HighScore { get; private set; }

    [SerializeField]
    public bool AchievedNewHighscore { get; private set; }

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text highscoreText;

    [SerializeField]
    private Text newHighscoreText;

    public void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore",0);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {Score}";
        }

        if (highscoreText != null)
        {
            highscoreText.text = $"Highscore: {HighScore}";
        }

        if (highscoreText != null)
        {
            newHighscoreText.gameObject.SetActive(AchievedNewHighscore);
        }
    }

    public void IncreaseScore()
    {
        ++Score;
        if(Score > HighScore)
        {
            HighScore = Score;
            AchievedNewHighscore = true;
        }

        UpdateScoreText();
    }

    public void ResetScore()
    {
        Score = 0;
        AchievedNewHighscore = false;
        UpdateScoreText();
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
        PlayerPrefs.Save();
    }
}
