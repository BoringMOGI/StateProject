using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallObject : MonoBehaviour
{
    GameManager manager;

    // ��ź�� ��Ȳ�� ���� ���� �Ŵ������� �˷�����ϱ� ������ �Ŵ����� �˰� �־�� �Ѵ�.
    public void Setup(GameManager manager)
    {
        this.manager = manager;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� ��ü���Լ� PlayerMove ������Ʈ�� �˻��Ѵ�.
        PlayerMove player = collision.gameObject.GetComponent<PlayerMove>();
        if(player != null)
        {
            Debug.Log("�÷��̾�� �浹��");
            manager.GameOver();
            Destroy(collision.gameObject);
        }
        else
        {
            manager.AddScore();
        }

        Destroy(gameObject);
    }

    // �� �������� �������� ��.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        manager.AddScore();
        Destroy(gameObject);
    }
}
