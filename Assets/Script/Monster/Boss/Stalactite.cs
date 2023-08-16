using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    public int dmg;
    public float downSpeed;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // 아래 방향으로 힘을 주는 로직 추가
        if (gameObject.activeSelf)
        {
            rb.AddForce(Vector2.down * downSpeed, ForceMode2D.Impulse); // 아래로 힘을 가하도록 조정 가능
        }
    }


    private void StalactiteBomb()
    {

    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerBehavior playerBehavior = collision.gameObject.GetComponent<PlayerBehavior>();

            if (playerBehavior != null)
            {
                playerBehavior.DecreaseHp(dmg);
            }

        }

        if (collision.gameObject.CompareTag("DownFloor"))
        {
            StalactiteBomb();
        }
    }

}
