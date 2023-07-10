using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic : Monster
{
    public bool findAttack;
    public float rayLength = 3f; // 감지 범위

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        ScanFront();
        if(!findAttack)
        {
            MoveMonster();
        }
        else 
        {
            Attack();
        }
    }


    private void ScanFront() // 전방에 플레이어, 혹은 하우스가 있는 확인하는 함수
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, rayLength, LayerMask.GetMask("Player") | LayerMask.GetMask("House"));

        Debug.DrawRay(transform.position, Vector2.left * rayLength, Color.red); // Ray의 시각적인 표시를 위한 Debug.DrawRay 사용

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("House"))
            {
                findAttack = true;  
            }
        }
        else
        {
            findAttack = false;  
        }
    }

    private void MoveMonster() // 좌측으로 이동하는 함수
    {
        anim.SetBool("isWalk", true);
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void Attack()
    {

    }
}
