using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ScoreUI scoreUI;
    public GameObject gameOverText;
    public bool isGameOver;
    public int score;      // Á¡¼ö.

    private void Start()
    {
        scoreUI.UpdateScore(score);
        gameOverText.SetActive(false);
    }

    public void AddScore()
    {
        if (isGameOver)
            return;

        score += 1;
        scoreUI.UpdateScore(score);
    }
    public void GameOver()
    {
        isGameOver = true;
        gameOverText.SetActive(true);
    }
}
