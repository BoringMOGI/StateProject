using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public ScoreUI scoreUI;
    public SceneMover sceneMover;       // 씬 관리자.
    public GameObject resultMenu;       // 결과창.
    public bool isGameOver;
    public int score;                   // 점수.

    private void Start()
    {
        scoreUI.UpdateScore(score);
        resultMenu.SetActive(false);
    }

    public void ReplayGame()
    {
        sceneMover.MoveScene("Fall");
        //SceneManager.LoadScene("Fall");
    }
    public void GoToMain()
    {
        sceneMover.MoveScene("Main");
        //SceneManager.LoadScene("Main");
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
        resultMenu.SetActive(true);
    }
}
