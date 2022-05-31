using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;

    public void UpdateScore(int score)
    {
        scoreText.text = string.Format("Á¡¼ö:{0}", score);
    }
}
