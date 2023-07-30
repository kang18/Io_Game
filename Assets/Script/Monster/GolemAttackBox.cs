using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttackBox : MonoBehaviour
{
    public int damage;
    public float knockbackForce; // 뒤쪽으로 밀어낼 힘의 크기
    public float knockbackUpForce; // 위로 밀어낼 힘의 크기

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerBehavior playerBehavior = collision.gameObject.GetComponent<PlayerBehavior>();
        House house = collision.gameObject.GetComponent<House>();

        if (playerBehavior != null)
        {
            playerBehavior.DecreaseHp(damage);

            playerBehavior.isRope = true;
            KnockbackPlayer(collision.transform.position);
        }
        
        

        if (house != null)
        {
            house.TakeDamage(damage);
        }

        void KnockbackPlayer(Vector3 playerPosition)
        {
            Vector3 knockbackDirection = (transform.position - playerPosition).normalized;
            Rigidbody2D playerRigidbody = playerBehavior.GetComponent<Rigidbody2D>();
            playerRigidbody.velocity = Vector2.zero;

            // 플레이어를 뒤쪽으로 밀어내는 힘과 위로 밀어내는 힘을 조합하여 적용합니다.
            Vector2 totalKnockbackForce = knockbackDirection * knockbackForce * (-1) + Vector3.up * knockbackUpForce;
            playerRigidbody.AddForce(totalKnockbackForce, ForceMode2D.Impulse);
        }
    }


    
}