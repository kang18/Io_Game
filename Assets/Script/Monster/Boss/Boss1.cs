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
    public float stunTime;
    public float jumpForce;
    public float jumpForceUnder;

    public int minX; // 최대 좌측 X 값
    public bool isminX; // 현재 최대한 좌측에 있는지에 따른 bool 값
    public int maxX; // 최대 좌측 X 값
    public bool ismaxX; // 현재 최대한 좌측에 있는지에 따른 bool 값
    public bool isJump; // 점프하고 있는지
    public bool isUnderJump; // 하향점프하고 있는지




    public int activeNumber; // 어떤 행동을 할 것인지에 대한 랜덤 번호
    public bool isActive; // 행동이 겹치지 않도록
    public bool isBress;
    public bool isSnakeAttack;
    public bool isStalactite;
    public bool isMedusa;

    public GameObject player;

    public GameObject pattren;
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
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(MoveLeftCoroutine());

        Invoke("ActivateBoss", 2f);
    }

    private void ActivateBoss()
    {
        player = GameObject.FindWithTag("Player");
        pattren = GameObject.Find("BossPattern");


        stalactitePosition[0] = GameObject.Find("Position_1").transform;
        stalactitePosition[1] = GameObject.Find("Position_2").transform;
        stalactitePosition[2] = GameObject.Find("Position_3").transform;
        stalactitePosition[3] = GameObject.Find("Position_4").transform;
        stalactitePosition[4] = GameObject.Find("Position_5").transform;
        stalactitePosition[5] = GameObject.Find("Position_6").transform;
        stalactitePosition[6] = GameObject.Find("Position_7").transform;
        stalactitePosition[7] = GameObject.Find("Position_8").transform;
        stalactitePosition[8] = GameObject.Find("Position_9").transform;
        stalactitePosition[9] = GameObject.Find("Position_10").transform;
        stalactitePosition[10] = GameObject.Find("Position_11").transform;

        stalactiteAlrm[0] = GameObject.Find("Position_1_alrm");
        stalactiteAlrm[1] = GameObject.Find("Position_2_alrm");
        stalactiteAlrm[2] = GameObject.Find("Position_3_alrm");
        stalactiteAlrm[3] = GameObject.Find("Position_4_alrm");
        stalactiteAlrm[4] = GameObject.Find("Position_5_alrm");
        stalactiteAlrm[5] = GameObject.Find("Position_6_alrm");
        stalactiteAlrm[6] = GameObject.Find("Position_7_alrm");
        stalactiteAlrm[7] = GameObject.Find("Position_8_alrm");
        stalactiteAlrm[8] = GameObject.Find("Position_9_alrm");
        stalactiteAlrm[9] = GameObject.Find("Position_10_alrm");
        stalactiteAlrm[10] = GameObject.Find("Position_11_alrm");



        foreach (GameObject stalactite in stalactiteAlrm)
        {
            stalactite.SetActive(false);
            SpriteRenderer spriteRenderer3 = stalactite.GetComponent<SpriteRenderer>();
            if (spriteRenderer3 != null)
            {
                Color color = spriteRenderer3.color;
                color.a = 1.0f;
                spriteRenderer3.color = color;
            }
        }
    }

    private void Update()
    {
        UpdateLayer();

        if (!isActive)
        {
            MakeActiveNumber();

            switch (activeNumber)
            {
                case 0:
                    StartCoroutine(MoveRightCoroutine());
                    break;
                case 1:
                    StartCoroutine(MoveLeftCoroutine());
                    break;
                case 2:
                    if (whereFloor)
                    {
                        StartCoroutine(JumpToUnder());
                        break;
                    }
                    else
                    {
                        StartCoroutine(JumpToUpArray());
                        break;
                    }
                case 3:
                    StartCoroutine(SnakeAttack());
                    break;
                case 4:
                    StartCoroutine(Bress());
                    break;
                case 5:
                    StartCoroutine(SpawnStalactites());
                    break;
                case 6:
                    StartCoroutine(Medusa());
                    break;

                default:
                    break;
            }

        }

    }



    private void MakeActiveNumber() // 랜덤 행동 난수 생성
    {
        activeNumber = Random.Range(5, 6);

        // 0 - 왼쪽으로 이동
        // 1 - 오른쪽으로 이동
        // 2 - 점프 || 하단 점프  (위층에 있는지 아래층에 있는지에 따라 행동 결정)
        // 3 - 공격 1
        // 4 - 공격 2
        // 5 - 공격 3
        // 6 - 공격 4
    }


    private IEnumerator MoveRightCoroutine() // 오른쪽으로 이동하는 코루틴
    {
        isActive = true;

        float moveDuration = Random.Range(0f, 3f);
        float elapsedTime = 0.0f;

        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        while (elapsedTime < moveDuration)
        {
            if (ismaxX)
            {
                isActive = false;
                yield break;
            }
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            JudgmentTransform();
            elapsedTime += Time.deltaTime;

            yield return null;
        }


        yield return new WaitForSeconds(1f);
        isActive = false;
    }

    private IEnumerator MoveLeftCoroutine() // 왼쪽으로 이동하는 코루틴
    {
        isActive = true;

        float moveDuration = Random.Range(0f, 3f);
        float elapsedTime = 0.0f;

        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        while (elapsedTime < moveDuration)
        {
            if (isminX)
            {
                isActive = false;
                yield break;
            }
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            JudgmentTransform();
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        yield return new WaitForSeconds(1f);
        isActive = false;
    }

    private void JudgmentTransform()
    {
        if (transform.position.x <= minX)
        {
            isminX = true;
        }
        else
        {
            isminX = false;
        }

        if (transform.position.x >= maxX)
        {
            ismaxX = true;
        }
        else
        {
            ismaxX = false;
        }
    }// 현재 위치가 최좌우측 끝인지 판단하는 함수

    IEnumerator JumpToUpArray()
    {
        isActive = true;

        yield return new WaitForSeconds(0.5f);

        isJump = true;
        rigid.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.9f);

        isJump = false;

        yield return new WaitForSeconds(2f);

        isActive = false;


    } // 상단 점프

    IEnumerator JumpToUnder() // 하단 점프
    {
        isActive = true;

        yield return new WaitForSeconds(0.5f);

        isJump = true;
        isUnderJump = true;
        rigid.AddForce(Vector3.up * jumpForceUnder, ForceMode2D.Impulse);

        yield return new WaitForSeconds(3f);

        isActive = false;
    }




    private void UpdateLayer()
    {
        if(isJump || isUnderJump)
        {
            gameObject.layer = LayerMask.NameToLayer("MonsterRope");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Monster");
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
    private void RandomPositionCount() // 종유석이 떨어질 랜덤 포인트 4곳 지정
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
                // 보스와 플레이어가 바라보고 있는 방향에 따라 bool 값 수정
                SeeUpdate();
                playerBehavior.SeeUpdate();

                if (whereFloor == playerBehavior.positionUpDown && whereSee == playerBehavior.iswhereSee)
                {
                    playerBehavior.hp -= medusaDmg;
                    StartCoroutine(playerBehavior.Stun(stunTime));
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
