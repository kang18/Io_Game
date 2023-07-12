using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Flying : Monster
{
    public bool findAttack; // 적을 찾음
    public bool isAttack; // 공격 중
    public float rayLength; // 감지 범위
    public float bulletSpeed; // 총알 날아가는 속도

    public bool up; // 위에 있는지 아래에 있는지

    public GameObject flyingBullet; // 발사할 총알 프리펩
    public GameObject step; // 공중에 떠 있는 느낌을 주기 위한 발판

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {
        if (hp <= 0 && !dodie)
        {
            StartCoroutine(Die());
        }

        if (!dodie)
        {
            if (!isAttack && !findAttack)
            {
                 MoveMonster();
            }
            ScanFront();
        }
    }


    private void ScanFront() // 전방에 플레이어, 혹은 하우스가 있는 확인하는 함수
    {
        float yOffset = -3.0f; // Y 좌표의 오프셋 값

        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, yOffset, 0), Vector2.left, rayLength, LayerMask.GetMask("Player") | LayerMask.GetMask("House"));

        Debug.DrawRay(transform.position + new Vector3(0, yOffset, 0), Vector2.left * rayLength, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("House"))
            {
                findAttack = true;
                if(!isAttack)
                {
                    StartCoroutine(FireBullet(hit.point));
                }
            }
        }
        else
        {
            findAttack = false;
        }
    }


    private void MoveMonster() // 좌측으로 이동하는 함수
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }


    IEnumerator FireBullet(Vector3 targetPosition)
    {
        anim.SetBool("isAttack", true);
        isAttack = true;
        yield return new WaitForSeconds(0.7f);

        // 발사할 총알 생성
        GameObject bullet = Instantiate(flyingBullet, transform.position, Quaternion.identity);

        // 총알의 방향 설정
        Vector3 direction = (targetPosition - transform.position).normalized;

        if(up)
        {
            float yOffset = -5f;
            Vector3 startPosition = transform.position + new Vector3(0f, yOffset, 0f);
            bullet.GetComponent<Rigidbody2D>().velocity = (targetPosition - startPosition).normalized * bulletSpeed;
        }
        else
        {
            // 총알의 속도 설정
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            yield return new WaitForSeconds(0.2f);
        }
        

        anim.SetBool("isAttack", false);

        yield return new WaitForSeconds(0.2f);

        isAttack = false;
    }




    IEnumerator Die()
    {
        dodie = true;
        anim.SetTrigger("doDie");
        step.SetActive(false);

        // 콜라이더 비활성화
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;

        // Rigidbody2D 움직임 비활성화
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
        rigidbody.angularVelocity = 0f;
        rigidbody.simulated = false;

        yield return new WaitForSeconds(1f);  // 죽음 함수 작동 후, 오브젝트가 삭제되는 시간, 애니메이션 출력 시간이랑 맞춰야 함
        Destroy(gameObject);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("UpArray"))
        {
            up = true;
        }
    }

}
