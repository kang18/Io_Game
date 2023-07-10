using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpArray : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rigidbody = other.GetComponent<Rigidbody2D>();
        if (rigidbody != null)
        {
            Vector2 force = new Vector2(0, -8f); 
            rigidbody.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerBehavior playerMovement = other.gameObject.GetComponent<PlayerBehavior>();

        if (playerMovement != null)
        {
            playerMovement.positionUpDown = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerBehavior playerMovement = other.gameObject.GetComponent<PlayerBehavior>();

        if (playerMovement != null)
        {
            playerMovement.positionUpDown = false;
        }
    }
}
