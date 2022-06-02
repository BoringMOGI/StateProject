using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    public ParticleSystem explodeParticle;

    GameManager manager;

    // 폭탄은 상황에 따라서 게임 매니저에게 알려줘야하기 때문에 매니저를 알고 있어야 한다.
    public void Setup(GameManager manager)
    {
        this.manager = manager;
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = Random.Range(0.5f, 1.5f);

        //rigid.velocity = new Vector2(0.0f, Random.Range(0.0f, 3.0f) * -1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌한 물체에게서 PlayerMove 컴포넌트를 검색한다.
        PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
        if(player != null)
        {
            if (player.isDead == false)
            {
                Debug.Log("플레이어와 충돌함");
                manager.GameOver();
                player.OnDead();
            }
        }
        else
        {
            manager.AddScore();
        }

        SetParticle();
        Destroy(gameObject);
    }

    // 빈 공간으로 떨어졌을 때.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.AddScore();
        Destroy(gameObject);
    }

    private void SetParticle()
    {
        ParticleSystem particle = Instantiate(explodeParticle, transform.position, Quaternion.identity);
        particle.Play();
    }
}
