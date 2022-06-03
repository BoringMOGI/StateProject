using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;
    public Text highScoreText;

    public void UpdateHighScore(int amount)
    {
        highScoreText.text = amount.ToString();
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
