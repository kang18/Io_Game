using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public int attackDamage; // 공격력
    public int bulletNumber; // 현재 발사하는 총알의 번호수(총알 종류)
    public float attackSpeed; // 공격 스피드
    private float attackTimer = 0f;
    public bool positionUpDown;  // 플레이어가 위에 있는지 아래에 있는지


    private bool keyUp; // 위 방향키가 눌렸는지
    private bool keyDown; // 아래 방향키가 눌렸는지
    private bool isAttack; // 공격 진행 중인지


    public Transform positionUp;
    public Transform positionDown;
    public Transform bulletPosition;
    public GameObject[] bullet; // 총알 프리팹 담을 배열


    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GetInput();
        Move();
    }

    private void FixedUpdate()
    {
        attackTimer += Time.fixedDeltaTime;
        if (attackTimer >= attackSpeed)
        {
            Attack();
            attackTimer = 0f;
        }
    }


    private void GetInput()  // 키 입력 받는 함수
    {
        keyUp = Input.GetKey(KeyCode.UpArrow);
        keyDown = Input.GetKey(KeyCode.DownArrow);
        isAttack = Input.GetButton("Attack");
    }

    private void Move() // 플레이어 위치 이동 함수
    {
        if (keyUp && !positionUpDown)
        {
            transform.position = positionUp.position;
            positionUpDown = true;
        }

        if (keyDown && positionUpDown)
        {
            transform.position = positionDown.position;
            positionUpDown = false;
        }
    }

    private void Attack() // 총알 발사 함수
    {
        if (isAttack)
        {
            GameObject shotbullet = Instantiate(bullet[bulletNumber], bulletPosition.position, Quaternion.identity);

            shotbullet.transform.localScale = new Vector3(-shotbullet.transform.localScale.x, shotbullet.transform.localScale.y, shotbullet.transform.localScale.z);

            Rigidbody2D projectileRigidbody = shotbullet.GetComponent<Rigidbody2D>();
            if (projectileRigidbody != null)
            {
                projectileRigidbody.AddForce(Vector2.left * 20, ForceMode2D.Impulse);
            }
        }
    }
}
