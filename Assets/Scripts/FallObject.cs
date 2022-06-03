using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    public ParticleSystem explodeParticle;
    public int addScore;

    GameManager manager;

    // ��ź�� ��Ȳ�� ���� ���� �Ŵ������� �˷�����ϱ� ������ �Ŵ����� �˰� �־�� �Ѵ�.
    public void Setup(GameManager manager)
    {
        this.manager = manager;
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.gravityScale = Random.Range(0.5f, 1.5f);

        //rigid.velocity = new Vector2(0.0f, Random.Range(0.0f, 3.0f) * -1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ��ü���Լ� PlayerMove ������Ʈ�� �˻��Ѵ�.
        PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
        if (player != null && player.isDead == false)
        {
            manager.GameOver();
            player.OnDead();
        }
        else
        {
            manager.AddScore(addScore);
        }

        SetParticle();
        Destroy(gameObject);
    }

    // �� �������� �������� ��.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.AddScore(addScore);
        Destroy(gameObject);
    }

    private void SetParticle()
    {
        ParticleSystem particle = Instantiate(explodeParticle, transform.position, Quaternion.identity);
        particle.Play();
    }
}
