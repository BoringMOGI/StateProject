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

        // time�� �ð��� ���ϰ� ���ϴ� �ð��� �Ǳ� ������ �ݺ�.
        while ((time -= Time.deltaTime) > 0.0f)
        {
            ChangeAlpha(time / fadeTime);   // ������ ���� ���İ� ����.
            yield return null;              // 1������ ����.
        }

        ChangeAlpha(0f);
        blindImage.enabled = false;
    }
    IEnumerator FadeOut(string sceneName, float fadeTime = 1.0f)
    {
        float time = 0.0f;
        blindImage.enabled = true;

        // time�� �ð��� ���ϰ� ���ϴ� �ð��� �Ǳ� ������ �ݺ�.
        while((time += Time.deltaTime) < fadeTime)
        {
            ChangeAlpha(time / fadeTime);   // ������ ���� ���İ� ����.
            yield return null;              // 1������ ����.
        }

        ChangeAlpha(1f);
        SceneManager.LoadScene(sceneName);
    }

    void FadeOut2(string sceneName, float fadeTime = 1.0f)
    {
        float time = 0.0f;
        blindImage.enabled = true;

        // time�� �ð��� ���ϰ� ���ϴ� �ð��� �Ǳ� ������ �ݺ�.
        while ((time += Time.deltaTime) < fadeTime)
        {
            ChangeAlpha(time / fadeTime);   // ������ ���� ���İ� ����.
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
