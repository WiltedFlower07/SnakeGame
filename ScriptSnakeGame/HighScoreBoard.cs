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
        int highScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();
        }
    }
}
