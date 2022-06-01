using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    public GameManager manager;
    public FallObject prefab;
    public Transform p1;
    public Transform p2;
    public float respawnTime;

    float nextSpawnTime;

    private void Update()
    {
        // Time.time : ������ ����ǰ� ���ݱ��� �帥 �ð�.
        // ���� �Ŵ����� isGamOver�� false�̰� ������ Ÿ���� �Ǿ��ٸ�.
        if(!manager.isGameOver && nextSpawnTime <= Time.time)
        {
            nextSpawnTime = Time.time + respawnTime;
            CreateFall();
        }
    }


    private void CreateFall()
    {
        float x = Random.Range(p1.position.x, p2.position.x);
        Vector2 position = new Vector2(x, p1.position.y);

        // Quaternion.identity : ���� �������� ������ �ִ� ȸ������ ���ڴ�.
        FallObject fallObject = Instantiate(prefab, position, Quaternion.identity);
        fallObject.Setup(manager);
    }

    
}
