using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PlayerCannon player;

    public void Setup(PlayerCannon player)
    {
        this.player = player;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        switch(tag)
        {
            case "Coin":
            case "Player":
                Debug.Log("코인 1 증가");
                player.AddCoin(1);
                break;
            case "Ghost":
                Debug.Log("코인 1 감소");
                player.DeleteCoin(1);
                break;
        }

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
