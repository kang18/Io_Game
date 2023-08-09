using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public int hp;
    public int touchDmg;
    public float speed;
    public bool dodie;
    public bool whereFloor;
    public bool whereSee;
    public bool isTargetOnRight;
    public int numberOfStalactitesToSpawn = 4;
    public int medusaDmg;

 
    public bool isActive; // 행동이 겹치지 않도록
    public bool isBress;
    public bool isSnakeAttack;
    public bool isStalactite;
    public bool isMedusa;

    public GameObject player;

    public GameObject bressUp;
    public GameObject bressDown;
    public GameObject snakeAttack;
    public GameObject stalactite;
    public Transform[] stalactitePosition;
    public GameObject[] stalactiteAlrm;
    int randomPosition1;
    int randomPosition2;
    int randomPosition3;
    int randomPosition4;
    public GameObject[] medusaEye;


    Animator anim;




    private void Update()
    {
        anim = GetComponent<Animator>();
        SeeUpdate();

        if (!isMedusa)
        {
            StartCoroutine(Medusa());
        }
    }





    IEnumerator SnakeAttack() // 나중에 애니메이션에 맞춰서 코루틴 타이밍 수정해야 함
    {
        isActive = true;
        isSnakeAttack = true;
        FlipTowardsPlayerDirection();

        yield return new WaitForSeconds(0.5f);

        snakeAttack.SetActive(true);

        yield return new WaitForSeconds(1.0f);

        snakeAttack.SetActive(false);

        isActive = false;
        isSnakeAttack = false;
    }


    IEnumerator Bress() // 나중에 애니메이션에 맞춰서 코루틴 타이밍 수정해야 함
    {
        isActive = true;
        isBress = true;
        FlipTowardsPlayerDirection();

        yield return new WaitForSeconds(0.5f);

        // 1층의 있을 때와 2층에 있을 때의 구분을 위해서
        if(whereFloor) { bressDown.SetActive(true); } 
        else { bressUp.SetActive(true); }

        yield return new WaitForSeconds(1.0f);

        if (whereFloor) { bressDown.SetActive(false); } 
        else { bressUp.SetActive(false); }

        isActive = false;
        isBress = false;
    }

    IEnumerator SpawnStalactites()
    {
        isActive = true;
        isStalactite = true;
        RandomPositionCount();

        yield return new WaitForSeconds(0.5f);

        stalactiteAlrm[randomPosition1].SetActive(true);
        stalactiteAlrm[randomPosition2].SetActive(true);
        stalactiteAlrm[randomPosition3].SetActive(true);
        stalactiteAlrm[randomPosition4].SetActive(true);

        yield return new WaitForSeconds(1.2f);

        GameObject stalactite_1 = Instantiate(stalactite, stalactitePosition[randomPosition1].position, Quaternion.identity);
        GameObject stalactite_2 = Instantiate(stalactite, stalactitePosition[randomPosition2].position, Quaternion.identity);
        GameObject stalactite_3 = Instantiate(stalactite, stalactitePosition[randomPosition3].position, Quaternion.identity);
        GameObject stalactite_4 = Instantiate(stalactite, stalactitePosition[randomPosition4].position, Quaternion.identity);

        yield return new WaitForSeconds(0.5f);

        stalactiteAlrm[randomPosition1].SetActive(false);
        stalactiteAlrm[randomPosition2].SetActive(false);
        stalactiteAlrm[randomPosition3].SetActive(false);
        stalactiteAlrm[randomPosition4].SetActive(false);

        yield return new WaitForSeconds(1.0f);

        isActive = false;
        isStalactite = false;
    }

    private void RandomPositionCount()
    {
        randomPosition1 = Random.Range(0, 11);
        randomPosition2 = Random.Range(0, 11);
        while(randomPosition1 == randomPosition2)
        {
            randomPosition2 = Random.Range(0, 11);
        }
        randomPosition3 = Random.Range(0, 11);
        while (randomPosition1 == randomPosition3 || randomPosition2 == randomPosition3)
        {
            randomPosition3 = Random.Range(0, 11);
        }
        randomPosition4 = Random.Range(0, 11);
        while (randomPosition1 == randomPosition4 || randomPosition2 == randomPosition4 || randomPosition3 == randomPosition4)
        {
            randomPosition4 = Random.Range(0, 11);
        }

        Debug.Log(randomPosition1);
        Debug.Log(randomPosition2);
        Debug.Log(randomPosition3);
        Debug.Log(randomPosition4);
    }



    IEnumerator Medusa()
    {
        isMedusa = true;
        isActive = true;
        FlipTowardsPlayerDirection();

        yield return new WaitForSeconds(0.2f);

        medusaEye[0].SetActive(true);

        yield return new WaitForSeconds(1.5f);

        medusaEye[0].SetActive(false);
        medusaEye[1].SetActive(true);

        isTargetOnRight = IsPlayerOnRight();

        if (!isTargetOnRight)
        {
            // 여기서 PlayerBehavior 컴포넌트를 가져오고 DecreaseHp() 함수를 호출
            PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();

            if (playerBehavior != null)
            {
                if (whereFloor == playerBehavior.positionUpDown)
                {
                    playerBehavior.DecreaseHp(medusaDmg); // medusaDmg는 Medusa의 공격력이라고 가정
                }
            }
        }

        yield return new WaitForSeconds(1.0f);

        medusaEye[1].SetActive(false);

        yield return new WaitForSeconds(1.0f);

        isMedusa = false;
        isActive = false;
    }

    private void SeeUpdate()
    {
        whereSee = transform.localScale.x < 0;
    }

    private bool IsPlayerOnRight()
    {
        Vector3 playerDirection = player.transform.position - transform.position;

        if (transform.localScale.x > 0) // 보스 오브젝트가 오른쪽을 보고 있을 때
        {
            return playerDirection.x > 0;
        }
        else // 보스 오브젝트가 왼쪽을 보고 있을 때
        {
            return playerDirection.x < 0;
        }
    }






    private void FlipTowardsPlayerDirection() // 플레이어가 있는 방향으로 좌우반전하는 함수
    {
        Vector3 playerDirection = player.transform.position - transform.position;

        if (playerDirection.x < 0) // 플레이어가 오른쪽에 있을 때
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (playerDirection.x > 0) // 플레이어가 왼쪽에 있을 때
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }






    public SpawnManager spawnManager; // SpawnManager 스크립트의 참조를 저장할 변수

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();
        if (collision.gameObject.CompareTag("Bullet") && !dodie)
        {
            hp -= bullet.dmg;
            if (hp < 0)
            {
                hp = 0;
            }
        }

        PlayerBehavior playerBehavior = collision.gameObject.GetComponent<PlayerBehavior>();
        if (playerBehavior != null)
        {
            playerBehavior.DecreaseHp(touchDmg);
        }
    }
}
