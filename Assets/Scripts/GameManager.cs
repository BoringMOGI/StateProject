using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera cam;
    public PlayerMove Player;

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

        StartCoroutine(DeadMotion());
    }

    IEnumerator CameraMotion()
    {
        Vector3 destination = Player.transform.position;
        destination.z = -10f;

        // 카메라의 시야각 사이즈를 2로 줄이고 플레이어의 x,y축 위치와 동일하게 맞춘다. (시간에 따라)
        while(cam.transform.position != destination || cam.orthographicSize != 2f)
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, destination, 30f * Time.deltaTime);
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, 2f, 10f * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator DeadMotion()
    {
        Vector3 originCameraPos = cam.transform.position;
        float originCameraSize = cam.orthographicSize;

        Time.timeScale = 0.2f;
        yield return StartCoroutine(CameraMotion());    // 카메라 모션 코루틴이 끝날때까지 기다린다.
        yield return new WaitForSeconds(1f * 0.2f);

        Time.timeScale = 1.0f;
        cam.transform.position = originCameraPos;
        cam.orthographicSize = originCameraSize;

        resultMenu.SetActive(true);
    }
}
