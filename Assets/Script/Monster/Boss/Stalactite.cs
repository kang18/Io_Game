using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    public int dmg;
    public float downSpeed;

    public GameObject Sculpture_1;
    public GameObject Sculpture_2;
    public GameObject Sculpture_3;
    public Transform Position_1;
    public Transform Position_2;
    public Transform Position_3;

    public GameObject Smoke;
    public Transform Smoke_Position_1;
    public Transform Smoke_Position_2;

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
        Destroy(gameObject);

        GameObject Sculpture_1_ = Instantiate(Sculpture_1, Position_1.position, Quaternion.identity);
        Rigidbody2D rb1 = Sculpture_1_.GetComponent<Rigidbody2D>();
        if (rb1 != null)
        {
            Vector2 force = new Vector2(Random.Range(-15f, 15f), Random.Range(10f, 20f)); // 강한 힘으로 수정
            rb1.AddForce(force, ForceMode2D.Impulse);
        }

        GameObject Sculpture_2_ = Instantiate(Sculpture_2, Position_2.position, Quaternion.identity);
        Rigidbody2D rb2 = Sculpture_2_.GetComponent<Rigidbody2D>();
        if (rb2 != null)
        {
            Vector2 force = new Vector2(Random.Range(-15f, 15f), Random.Range(10f, 20f)); // 강한 힘으로 수정
            rb2.AddForce(force, ForceMode2D.Impulse);
        }

        GameObject Sculpture_3_ = Instantiate(Sculpture_3, Position_3.position, Quaternion.identity);
        Rigidbody2D rb3 = Sculpture_3_.GetComponent<Rigidbody2D>();
        if (rb3 != null)
        {
            Vector2 force = new Vector2(Random.Range(-15f, 15f), Random.Range(10f, 20f)); // 강한 힘으로 수정
            rb3.AddForce(force, ForceMode2D.Impulse);
        }


        GameObject smoke_1_ = Instantiate(Smoke, Smoke_Position_1.position, Quaternion.identity);
        GameObject smoke_2_ = Instantiate(Smoke, Smoke_Position_2.position, Quaternion.identity);
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
