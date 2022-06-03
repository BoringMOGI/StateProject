using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] GameObject[] childs;
    [SerializeField] Text leaderboardText;

    private void Start()
    {
        SwitchResult(false);
    }

    public void SetLeaderboard(int[] scores)
    {
        // text에 점수들을 다 기록해서 리더보드 텍스트에 대입한다.
        string text = string.Empty;
        for(int i = 0; i< scores.Length; i++)
            text += string.Format("{0}.{1}\n", i + 1, scores[i]);

        leaderboardText.text = text;
    }

    public void SwitchResult(bool isOn)
    {
        foreach (GameObject c in childs)
            c.SetActive(isOn);
    }    
}
