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
    public ResultUI resultMenu;         // 결과창.
    public bool isGameOver;
    public int score;                   // 점수.
    public int highScore;               // 최고 점수.

    private void Start()
    {
        // 게임 시작시에 최고 스코어 저장값 불러온다.
        // 이후에 UI에게 값을 전달.
        highScore = PlayerPrefs.GetInt("HighScore");
        scoreUI.UpdateHighScore(highScore);
        scoreUI.UpdateScore(score);
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

    public void AddScore(int amount)
    {
        if (isGameOver)
            return;

        // 스코어를 1점 획득한 후에 만약 현재 스코어가 최고 점수를 넘겼으면
        // 최고 점수를 갱신하고 UI도 갱신한다.
        score += amount;
        if(score > highScore)
        {
            highScore = score;
            scoreUI.UpdateHighScore(highScore);
        }

        scoreUI.UpdateScore(score);
    }
    public void GameOver()
    {
        isGameOver = true;

        // 스코어와 최고 스코어가 같다면 최고점을 갱신했다는 말이니
        // 최고점을 저장.
        if(score == highScore)
            PlayerPrefs.SetInt("HighScore", score);

        StartCoroutine(DeadMotion());
    }

    IEnumerator CameraMotion()
    {
        float originZoomSize = cam.orthographicSize;    // 원래 줌 사이즈.
        float zoomSize = 2f;                            // 줌 되는 사이즈.

        Vector3 origoinPos = cam.transform.position;        // 원래 위치.
        Vector3 destination = Player.transform.position;    // 이동 목적지.
        destination.z = cam.transform.position.z;

        // 총 변화량.
        float distanceDelta = Vector3.Distance(cam.transform.position, destination);
        float sizeDelta = (cam.orthographicSize - zoomSize);

        // 걸리는 시간.
        float motionTime = 2f * Time.timeScale;
        float time = 0f;

        // 카메라의 시야각 사이즈를 2로 줄이고 플레이어의 x,y축 위치와 동일하게 맞춘다. (시간에 따라)
        while (time < motionTime)
        {
            time += Time.deltaTime;
            if (time >= motionTime)
                time = motionTime;

            float ratio = time / motionTime;

            cam.transform.position = Vector3.MoveTowards(origoinPos, destination, distanceDelta * ratio);
            cam.orthographicSize = Mathf.MoveTowards(originZoomSize, zoomSize, sizeDelta * ratio);
            yield return null;
        }
    }
    IEnumerator DeadMotion()
    {
        Time.timeScale = 0.2f;
        yield return StartCoroutine(CameraMotion());    // 카메라 모션 코루틴이 끝날때까지 기다린다.
        yield return new WaitForSeconds(0.5f * Time.timeScale);
        Time.timeScale = 1.0f;

        resultMenu.SwitchResult(true);

        // 값 불러오기.
        int score1 = PlayerPrefs.GetInt("Score1", 0);
        int score2 = PlayerPrefs.GetInt("Score2", 0);
        int score3 = PlayerPrefs.GetInt("Score3", 0);

        // 정렬.
        int[] array = SortDesending(new int[] { score1, score2, score3, score });
        score1 = array[0];
        score2 = array[1];
        score3 = array[2];

        // 값 저장.
        PlayerPrefs.SetInt("Score1", score1);
        PlayerPrefs.SetInt("Score2", score2);
        PlayerPrefs.SetInt("Score3", score3);

        // 결과창에게 점수 배열 전달.
        resultMenu.SetLeaderboard(new int[] { score1, score2, score3 });
    }

    private int[] SortDesending(int[] array)
    {
        // 인터페이스를 람다식으로 구현했지만, "람다식"은 다음에 설명.
        System.Array.Sort(array, (a, b) => {

            // 1은 우측, -1은 좌측, 0은 값이 동일하다.
            if (a < b)
                return 1;
            else if (a > b)
                return -1;
            else
                return 0;
        });

        return array;
    }
}
