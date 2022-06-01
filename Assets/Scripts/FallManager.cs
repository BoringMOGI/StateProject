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
        // Time.time : 게임이 실행되고 지금까지 흐른 시간.
        // 게임 매니저의 isGamOver가 false이고 리스폰 타임이 되었다면.
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

        // Quaternion.identity : 원래 프리팹이 가지고 있는 회전값을 쓰겠다.
        FallObject fallObject = Instantiate(prefab, position, Quaternion.identity);
        fallObject.Setup(manager);
    }

    
}
