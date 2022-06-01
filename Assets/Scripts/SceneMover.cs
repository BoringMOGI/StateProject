using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    public Image blindImage;

    private void Start()
    {
        StartCoroutine(FadeIn(1f));
    }

    public void MoveScene(string name)
    {
        StartCoroutine(FadeOut(name, 1f));
    }
    IEnumerator FadeIn(float fadeTime = 1.0f)
    {
        float time = fadeTime;
        blindImage.enabled = true;

        // time에 시간을 더하고 원하는 시간이 되기 전까지 반복.
        while ((time -= Time.deltaTime) > 0.0f)
        {
            ChangeAlpha(time / fadeTime);   // 비율에 따른 알파값 변경.
            yield return null;              // 1프레임 쉰다.
        }

        ChangeAlpha(0f);
        blindImage.enabled = false;
    }
    IEnumerator FadeOut(string sceneName, float fadeTime = 1.0f)
    {
        float time = 0.0f;
        blindImage.enabled = true;

        // time에 시간을 더하고 원하는 시간이 되기 전까지 반복.
        while((time += Time.deltaTime) < fadeTime)
        {
            ChangeAlpha(time / fadeTime);   // 비율에 따른 알파값 변경.
            yield return null;              // 1프레임 쉰다.
        }

        ChangeAlpha(1f);
        SceneManager.LoadScene(sceneName);
    }

    void FadeOut2(string sceneName, float fadeTime = 1.0f)
    {
        float time = 0.0f;
        blindImage.enabled = true;

        // time에 시간을 더하고 원하는 시간이 되기 전까지 반복.
        while ((time += Time.deltaTime) < fadeTime)
        {
            ChangeAlpha(time / fadeTime);   // 비율에 따른 알파값 변경.
        }

        ChangeAlpha(1f);
        SceneManager.LoadScene(sceneName);
    }


    private void ChangeAlpha(float value)
    {
        Color color = blindImage.color;
        color.a = value;
        blindImage.color = color;
    }
}
