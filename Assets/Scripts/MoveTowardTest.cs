using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardTest : MonoBehaviour
{
    public Vector2 destination;

    float movement;
    bool isMove;


    private void Update()
    {
        if (!isMove)
        {
            if (Input.GetKeyDown(KeyCode.S))
                Move();

            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, destination, movement * Time.deltaTime);
    }

    public void Move()
    {
        movement = Vector2.Distance(transform.position, destination);
        isMove = true;
    }

}
