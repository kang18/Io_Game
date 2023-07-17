using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public float spawnSpeed; // 생성주기
    private float spawnTimer = 0f;


    public GameObject[] Monsters;
    public Transform[] spawnPositionUpDown;


    private void FixedUpdate()
    {
        spawnTimer += Time.fixedDeltaTime;
        if (spawnTimer >= spawnSpeed)
        {
            SpawnMonster();
            spawnTimer = 0f;
        }
    }

    private void SpawnMonster()
    {
        int randomPosition = Random.Range(0, 2);
        int randomMonster = Random.Range(0, 4);

        Transform spawnPosition = spawnPositionUpDown[randomPosition];
        GameObject monster = Instantiate(Monsters[randomMonster], spawnPosition.position, Quaternion.identity);
        
    }

}
