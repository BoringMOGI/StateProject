using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rigid;
    public float moveSpeed;

    void Update()
    {
        Vector2 velocity = rigid.velocity;
        velocity.x = Input.GetAxis("Horizontal") * moveSpeed;

        rigid.velocity = velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Dead")
            Destroy(gameObject);
    }
}
