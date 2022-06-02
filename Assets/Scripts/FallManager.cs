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
        // Time.time : 게임이 실행되고 지금까지 흐른 시간.
        // 게임 매니저의 isGamOver가 false이고 리스폰 타임이 되었다면.
        if(!manager.isGameOver && nextSpawnTime <= Time.time)
        {
            nextSpawnTime = Time.time + respawnTime;
            CreateFall();
        }
    }

    private FallObject GetFallObject()
    {
        // 후보들의 전체 확률을 더한다.
        int total = 0;
        foreach (Bomb bomb in bombs)
            total += bomb.persent;

        // Random.value : 0.0 ~ 1.0 사이 값.
        // 랜덤 비율 값을 전체 확률에 더해서 원하는 지점을 산출한다.
        float pick = total * Random.value;
        float persent = 0;

        // 전체 배열을 돌면서 원하는 지점에 해당하는 후보를 반환한다.
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

        // Quaternion.identity : 원래 프리팹이 가지고 있는 회전값을 쓰겠다.
        FallObject fallObject = Instantiate(GetFallObject(), position, Quaternion.identity);
        fallObject.Setup(manager);
    }

    
}
