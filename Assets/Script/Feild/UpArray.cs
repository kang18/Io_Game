using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpArray : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rigidbody = other.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                Vector2 force = new Vector2(0, -20f);
                rigidbody.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }

    
     
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehavior playerMovement = other.gameObject.GetComponent<PlayerBehavior>();

            if (playerMovement != null)
            {
                playerMovement.positionUpDown = true;
            }
        }

        if (other.CompareTag("Monster"))
        {
            Boss1 boss = other.gameObject.GetComponent<Boss1>();

            if (boss != null)
            {
                boss.whereFloor = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehavior playerMovement = other.gameObject.GetComponent<PlayerBehavior>();

            if (playerMovement != null)
            {
                playerMovement.positionUpDown = false;
                playerMovement.isUnderJump = false;
            }
        }

        if (other.CompareTag("Monster"))
        {
            Boss1 boss = other.gameObject.GetComponent<Boss1>();

            if (boss != null)
            {
                boss.whereFloor = false;
                boss.isUnderJump = false;
            }
        }
    }
}
