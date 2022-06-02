using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallManager : MonoBehaviour
{
    [System.Serializable]
    public struct Bomb
    {
        public FallObject prefab;
        public int persent;
    }

    public GameManager manager;
    public Bomb[] bombs;

    [Header("Respawn")]
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

    private FallObject GetFallObject()
    {
        // �ĺ����� ��ü Ȯ���� ���Ѵ�.
        int total = 0;
        foreach (Bomb bomb in bombs)
            total += bomb.persent;

        // Random.value : 0.0 ~ 1.0 ���� ��.
        // ���� ���� ���� ��ü Ȯ���� ���ؼ� ���ϴ� ������ �����Ѵ�.
        float pick = total * Random.value;
        float persent = 0;

        // ��ü �迭�� ���鼭 ���ϴ� ������ �ش��ϴ� �ĺ��� ��ȯ�Ѵ�.
        for(int i = 0; i<bombs.Length; i++)
        {
            persent += bombs[i].persent;
            if(pick < persent)
                return bombs[i].prefab;
        }

        return null;
    }

    private void CreateFall()
    {
        float x = Random.Range(p1.position.x, p2.position.x);
        Vector2 position = new Vector2(x, p1.position.y);

        // Quaternion.identity : ���� �������� ������ �ִ� ȸ������ ���ڴ�.
        FallObject fallObject = Instantiate(GetFallObject(), position, Quaternion.identity);
        fallObject.Setup(manager);
    }

    
}
