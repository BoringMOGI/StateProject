using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    GameManager manager;

    // 폭탄은 상황에 따라서 게임 매니저에게 알려줘야하기 때문에 매니저를 알고 있어야 한다.
    public void Setup(GameManager manager)
    {
        this.manager = manager;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 물체에게서 PlayerMove 컴포넌트를 검색한다.
        PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
        if(player != null)
        {
            Debug.Log("플레이어와 충돌함");
            manager.GameOver();
            Destroy(collision.gameObject);
        }
        else
        {
            manager.AddScore();
        }

        Destroy(gameObject);
    }

    // 빈 공간으로 떨어졌을 때.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.AddScore();
        Destroy(gameObject);
    }
}
