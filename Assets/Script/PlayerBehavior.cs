using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;  // 이동 스피드
    public float jumpForce = 5f;  // 점프력
    public int bulletNumber; // 현재 발사하는 총알의 번호수(총알 종류)
    public float attackSpeed; // 공격 스피드
    private float attackTimer = 0f;

    public float moveX;  // 좌우 키 입력
    public bool movedown;  // 아래방향키 입력
    public bool keyJump; // 점프 키 입력
    public bool isJump; // 점프중인지
    private bool isAttack; // 공격 진행 중인지
    public bool isRope; // 로프를 타고 있는지
    public bool isUnderJump; // 하향 점프 하고 있는지

    public bool positionUpDown;


    private Rigidbody2D rigid;
    public Transform bulletPosition;
    public GameObject[] bullet; // 총알 프리팹 담을 배열

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        KeyInput();
        Move();
        UpdateLayer();
    }

    private void FixedUpdate()
    {
        attackTimer += Time.fixedDeltaTime;
        if (attackTimer >= attackSpeed && isAttack)
        {
            Attack();
            attackTimer = 0f;
        }
    }

    private void KeyInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        movedown = Input.GetKey(KeyCode.DownArrow);
        keyJump = Input.GetButtonDown("Jump");
        isAttack = Input.GetButton("Attack");
    }

    private void Move()
    {
        Vector3 movement = new Vector3(moveX, 0f, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // 이동 방향에 따라 오브젝트를 반전
        if (movement.x < 0) // 왼쪽으로 이동 중일 때
        {
            transform.localScale = new Vector3(-1.8f, 1.8f, 1f); // 오브젝트 크기가 수정됨
        }
        else if (movement.x > 0) // 오른쪽으로 이동 중일 때
        {
            transform.localScale = new Vector3(1.8f, 1.8f, 1f);
        }

        if (keyJump && !isJump)
        {
            isJump = true;

            if(movedown)
            {
                isUnderJump = true;
                rigid.AddForce(Vector3.up * jumpForce / 2, ForceMode2D.Impulse);
            }
            else
            {
                rigid.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }

            
        }
        else
        {
            rigid.AddForce(Vector3.down * 2.33f, ForceMode2D.Force);
        }
    }

    void Attack()
    {
        GameObject shotBullet = Instantiate(bullet[bulletNumber], bulletPosition.position, Quaternion.identity);
        Rigidbody2D rb = shotBullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float direction = transform.localScale.x > 0 ? 1f : -1f;
            rb.velocity = new Vector2(40f * direction, 0f);

            if (direction < 0) // 왼쪽 방향일 경우 프리펩을 반전시킴
            {
                shotBullet.transform.localScale = new Vector3(-3f, 3f, 1f);  // 반전된 상태로 발사되는 총알의 크기 조절
            }
        }
    }

    void UpdateLayer()
    {
        if (isRope || isUnderJump)
        {
            gameObject.layer = LayerMask.NameToLayer("PlayerRope");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
}






