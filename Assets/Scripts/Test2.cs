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

    private void Start()
    {
        int[] array = new int[] { 30, 50, 20 , 4};
        System.Array.Sort(array, (a, b) => {

            if (a < b)
                return 1;
            else if (a > b)
                return -1;
            else
                return 0;
        });

        Debug.Log(string.Join(",", array));
    }

    private void Update()
    {
        Vector2 me = new Vector2(0f, 0f);
        Vector2 target = new Vector2(10f, 0f);

        //transform.position = Vector2.MoveTowards(transform.position, target, 10 * Time.deltaTime);
        //score = Mathf.MoveTowards(score, maxScore, 50 * Time.deltaTime);
        //Debug.Log(score);

        transform.position = Vector2.Lerp(transform.position, target, 2f * Time.deltaTime);
        Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
        if(Vector2.Distance(myPos, target) <= 2f * Time.deltaTime)
        {
            Debug.Log("¸ØÃá´Ù.");
            transform.position = target;
        }
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
