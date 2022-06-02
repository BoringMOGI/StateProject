using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public struct Box
    {
        public string itemName;
        public int persent;
    }

    Box[] boxs;

    float score = 0;
    float maxScore = 200;

    private void Update()
    {
        //score = Mathf.MoveTowards(score, maxScore, 10 * Time.deltaTime);
        score = Mathf.Lerp(score, maxScore, 10f);

        Debug.Log(score);
    }

    [ContextMenu("Test Move")]
    public void TestMove()
    {
        StartCoroutine(MoveSmooth());
    }

    IEnumerator MoveSmooth()
    {
        Camera cam = Camera.main;
        Transform camTrans = cam.transform;

        Vector3 destination = new Vector3(0, 0, -10f);
        cam.transform.position = new Vector3(5f, 0f, -10f);

        while(cam.transform.position != destination)
        {
            //cam.transform.position = Vector3.Lerp(cam.transform.position, destination, 1f * Time.deltaTime);

            camTrans.position = Vector3.MoveTowards(camTrans.position, destination, 1f * Time.deltaTime);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 2f, 1f * Time.deltaTime);

            yield return null;
        }
    }

    private string GetCacha()
    {
        int total = 0;
        foreach (Box box in boxs)
            total += box.persent;

        float pick = total * Random.value;
        float persent = 0;

        for(int i = 0; i<boxs.Length; i++)
        {
            persent += boxs[i].persent;
            if(pick < persent)
            {
                return boxs[i].itemName;
            }
        }

        return "Error";
    }

    
}
