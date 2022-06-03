using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Camera cam;
    public PlayerMove Player;

    public ScoreUI scoreUI;
    public SceneMover sceneMover;       // �� ������.
    public ResultUI resultMenu;         // ���â.
    public bool isGameOver;
    public int score;                   // ����.
    public int highScore;               // �ְ� ����.

    private void Start()
    {
        // ���� ���۽ÿ� �ְ� ���ھ� ���尪 �ҷ��´�.
        // ���Ŀ� UI���� ���� ����.
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

        // ���ھ 1�� ȹ���� �Ŀ� ���� ���� ���ھ �ְ� ������ �Ѱ�����
        // �ְ� ������ �����ϰ� UI�� �����Ѵ�.
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

        // ���ھ�� �ְ� ���ھ ���ٸ� �ְ����� �����ߴٴ� ���̴�
        // �ְ����� ����.
        if(score == highScore)
            PlayerPrefs.SetInt("HighScore", score);

        StartCoroutine(DeadMotion());
    }

    IEnumerator CameraMotion()
    {
        float originZoomSize = cam.orthographicSize;    // ���� �� ������.
        float zoomSize = 2f;                            // �� �Ǵ� ������.

        Vector3 origoinPos = cam.transform.position;        // ���� ��ġ.
        Vector3 destination = Player.transform.position;    // �̵� ������.
        destination.z = cam.transform.position.z;

        // �� ��ȭ��.
        float distanceDelta = Vector3.Distance(cam.transform.position, destination);
        float sizeDelta = (cam.orthographicSize - zoomSize);

        // �ɸ��� �ð�.
        float motionTime = 2f * Time.timeScale;
        float time = 0f;

        // ī�޶��� �þ߰� ����� 2�� ���̰� �÷��̾��� x,y�� ��ġ�� �����ϰ� �����. (�ð��� ����)
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
        yield return StartCoroutine(CameraMotion());    // ī�޶� ��� �ڷ�ƾ�� ���������� ��ٸ���.
        yield return new WaitForSeconds(0.5f * Time.timeScale);
        Time.timeScale = 1.0f;

        resultMenu.SwitchResult(true);

        // �� �ҷ�����.
        int score1 = PlayerPrefs.GetInt("Score1", 0);
        int score2 = PlayerPrefs.GetInt("Score2", 0);
        int score3 = PlayerPrefs.GetInt("Score3", 0);

        // ����.
        int[] array = SortDesending(new int[] { score1, score2, score3, score });
        score1 = array[0];
        score2 = array[1];
        score3 = array[2];

        // �� ����.
        PlayerPrefs.SetInt("Score1", score1);
        PlayerPrefs.SetInt("Score2", score2);
        PlayerPrefs.SetInt("Score3", score3);

        // ���â���� ���� �迭 ����.
        resultMenu.SetLeaderboard(new int[] { score1, score2, score3 });
    }

    private int[] SortDesending(int[] array)
    {
        // �������̽��� ���ٽ����� ����������, "���ٽ�"�� ������ ����.
        System.Array.Sort(array, (a, b) => {

            // 1�� ����, -1�� ����, 0�� ���� �����ϴ�.
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
