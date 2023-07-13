using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Path;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Basic : Monster
{
    public bool findAttack; // 적을 찾음
    public bool isAttack; // 공격 중
    public float rayLength = 2f; // 감지 범위

    public BoxCollider2D attackArea; // 공격 범위

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(hp <= 0 && !dodie)
        {
            StartCoroutine(Die());
        }
        if (!dodie)
        {
            if (!isAttack)
            {
                if (!findAttack)
                {
                    MoveMonster();
                }
                else
                {
                    StartCoroutine(Attack());
                }
            }
            ScanFront();
        }
    }


    private void MoveMonster() // 좌측으로 이동하는 함수
    {
        anim.SetBool("isWalk", true);
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }



    private void ScanFront() // 전방에 플레이어, 혹은 하우스가 있는 확인하는 함수
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, rayLength, LayerMask.GetMask("Player") | LayerMask.GetMask("House"));

        Debug.DrawRay(transform.position, Vector2.left * rayLength, Color.red); // Ray의 시각적인 표시를 위한 Debug.DrawRay 사용

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("House"))
            {
                anim.SetBool("isWalk", false);
                findAttack = true;  
            }
        }
        else
        {
            anim.SetBool("isWalk", true);
            findAttack = false;  
        }
    }

  
    IEnumerator Attack()
    {
        isAttack = true;
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(1.5f);
        attackArea.enabled = true;

        yield return new WaitForSeconds(0.3f);
        
        attackArea.enabled = false;
        

        isAttack = false;
        anim.SetBool("isAttack", false);
    }

    IEnumerator Die()
    {
        dodie = true;
        anim.SetTrigger("doDie");

        // 콜라이더 비활성화
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;

        // Rigidbody2D 움직임 비활성화
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0f;
        rigidbody.simulated = false;

        yield return new WaitForSeconds(1.15f);
        Destroy(gameObject);
    }
}






//switch (enemyType)
//{
//    case Type.A:
//        yield return new WaitForSeconds(0.2f);   // 애니메이션에 맞춰서 맞는 판정이 나도록 
//        meleeArea.enabled = true;

//        yield return new WaitForSeconds(1f);
//        meleeArea.enabled = false;

//        yield return new WaitForSeconds(1f);
//        break;

//    case Type.B:
//        yield return new WaitForSeconds(0.1f);
//        rigid.AddForce(transform.forward * 20, ForceMode.Impulse);
//        meleeArea.enabled = true;

//        yield return new WaitForSeconds(0.5f);
//        rigid.velocity = Vector3.zero;
//        meleeArea.enabled = false;

//        yield return new WaitForSeconds(2f);
//        break;

//    case Type.C:
//        yield return new WaitForSeconds(0.5f);
//        GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
//        Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
//        rigidBullet.velocity = transform.forward * 20;

//        yield return new WaitForSeconds(2f);
//        break;
//}