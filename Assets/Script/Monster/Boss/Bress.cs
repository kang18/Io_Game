using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bress : MonoBehaviour
{
    public int bressDmg;

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerBehavior playerBehavior = collision.gameObject.GetComponent<PlayerBehavior>();
            playerBehavior.DecreaseHp(bressDmg);
        }
    }
}
