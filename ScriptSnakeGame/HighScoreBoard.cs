using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreBoard : MonoBehaviour
{
    public int currentScore;

    void Start()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log("High Score: " + highScore);
    }

    public void SaveScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);using UnityEngine;
using UnityEngine.UI;

public class HighScoreBoard : MonoBehaviour
{
    public Text scoreText;      // UI hiện điểm hiện tại
    public Text highScoreText;  // UI hiện high score

    private int score = 0;
    private int highScore = 0;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateUI();
    }

    public void AddScore(int amount)
    {
        score += amount;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        highScoreText.text = "High Score: " + highScore;
    }
}

        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }
    }
}
