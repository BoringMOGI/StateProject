using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager manager;
    public Rigidbody2D rigid;
    public float moveSpeed;
    public float jumpPower;

    void Update()
    {
        Vector2 velocity = rigid.velocity;
        velocity.x = Input.GetAxisRaw("Horizontal") * moveSpeed;

        rigid.velocity = velocity;

        if (Input.GetKeyDown(KeyCode.Space))
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dead")
        {
            manager.GameOver();
            Destroy(gameObject);
        }
    }
}
