using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerBehavior : MonoBehaviour
{
    public float moveSpeed = 5f;  // 이동 스피드
    public float jumpForce = 5f;  // 점프력

    public float moveX;  // 좌우 키 입력
    public float moveY;  // 상하 키 입력
    public bool keyJump; // 점프 키 입력

    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        KeyInput();
        Move();
    }


    private void KeyInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        keyJump = Input.GetButtonDown("Jump");
    }


    private void Move()
    {
        Vector3 movement = new Vector3(moveX, 0f, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (keyJump)
        {
            rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}






