using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int dmg = 5;
    public BoxCollider2D dmgCollider;

    private void Start()
    {
        StartCoroutine(ActivateDmgCollider());
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

    private IEnumerator ActivateDmgCollider()
    {
        while (true) // 무한 루프
        {
            dmgCollider.enabled = true; // Collider를 활성화
            yield return new WaitForSeconds(0.2f); // 0.2초 대기
            dmgCollider.enabled = false; // Collider를 비활성화
            yield return new WaitForSeconds(0.2f); // 0.2초 대기
        }
    }
}
