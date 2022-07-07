using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary> Manages, persists and displays the player score. </summary>
public class ScoreManager : MonoBehaviour
{
    public int Score { get { return _score; } }
    public int HighScore { get { return _highScore; } }
    public bool AchievedNewHighscore { get { return _achievedNewHighScore; } }

    [SerializeField]
    [Range(0, 10000)]
    int _score;

    [SerializeField]
    [Range(0, 10000)]
    int _highScore;

    [SerializeField]
    bool _achievedNewHighScore;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    Text highscoreText;

    [SerializeField]
    Text newHighscoreText;

    public void Start()
    {
        _highScore = PlayerPrefs.GetInt("HighScore",0);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {_score}";
        }

        if (highscoreText != null)
        {
            highscoreText.text = $"Highscore: {_highScore}";
        }

        if (newHighscoreText != null)
        {
            newHighscoreText.text = $"New Highscore: {_highScore}";
            newHighscoreText.gameObject.SetActive(_achievedNewHighScore);
        }
    }

    public void IncreaseScore()
    {
        ++_score;
        if(_score > _highScore)
        {
            _highScore = _score;
            _achievedNewHighScore = true;
        }

        UpdateScoreText();
    }

    public void ResetScore()
    {
        _score = 0;
        _achievedNewHighScore = false;
        UpdateScoreText();
    }

    void OnDestroy()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
        PlayerPrefs.Save();
    }
}
