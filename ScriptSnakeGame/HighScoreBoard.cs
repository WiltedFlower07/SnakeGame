using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScoreBoard : MonoBehaviour
{
    // UnityHub không nhận diện được Text, phải dùng TMP_Text, TMPro 

    [SerializeField] private TMP_Text scoreText; // UI hiện điểm hiện tại 
    [SerializeField] private TMP_Text highScoreText; // UI hiện high score

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
        highScoreText.text = "Highest score: " + highScore;

        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }
}
