using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBomEffect : MonoBehaviour
{
    public float bombingTime;
    public int dmg = 50; // Adjust the damage value as needed

    private void Start()
    {
        StartCoroutine(ExplodeAfterDelay(bombingTime));
    }

    private IEnumerator ExplodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has the "Monster" tag
        if (other.CompareTag("Monster"))
        {
            // Get the Monster script component from the colliding object
            Monster monsterScript = other.GetComponent<Monster>();
            Boss1 boss = other.GetComponent<Boss1>();

            // Check if the Monster script component is not null
            if (monsterScript != null)
            {
                monsterScript.StartCoroutine("OnDamage");
                monsterScript.hp -= dmg;
            }

            if (boss != null)
            {
                boss.hp -= dmg;
            }
        }
    }
}