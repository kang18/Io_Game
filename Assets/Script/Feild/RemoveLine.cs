using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Monster") || collision.gameObject.CompareTag("Stalactite"))
        {
            Destroy(collision.gameObject);
        }
    }
}
