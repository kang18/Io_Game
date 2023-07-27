using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 기본공격 / 폭탄을 던지고 있음 / 라인클리어 궁긍기 사용시 is~~를 이용해서 현재 어떤 행동을 하고 있는지 구분해야함
// 현재는 레이저 발사만 되어 있음


public class PlayerBehavior : MonoBehaviour
{
    public int hp;
    public int dmg; // 공격력
    public bool isDamage; // 데미지를 입고 있는지
    public bool doDie; // 죽었는지 살았는지

    public float moveSpeed = 5f;  // 이동 스피드
    public float jumpForce = 5f;  // 점프력
    public int bulletNumber; // 현재 발사하는 총알의 번호수(총알 종류)
    public float attackSpeed; // 공격 스피드
    public int gemPoint;
    private float attackTimer = 0f;
    
    public float moveX;  // 좌우 키 입력
    public bool movedown;  // 아래방향키 입력
    public bool keyJump; // 점프 키 입력
    public bool keySkilla; // 1번 스킬 입력
    public bool keySkillb; // 2번 스킬 입력
    public bool keySkillc; // 3번 스킬 입력

    public bool isJump; // 점프중인지
    private bool isAttack; // 공격 진행 중인지
    public bool isRope; // 로프를 타고 있는지
    public bool isUnderJump; // 하향 점프 하고 있는지
    public bool positionUpDown; // 지금 상단에 있는지 하단에 있는지


    private bool isSkillc = false; // 3번 스킬 사용중인지 아닌지


    private Rigidbody2D rigid;
    public Transform bulletPosition;
    public GameObject[] bullet; // 총알 프리팹 담을 배열
    public GameObject gemBomb; // 스킬 1번에 사용될 프리팹
    public GameObject whiteScreen; // 스킬 3번에 사용될 프리팹
    public GameObject whiteScreenDmg; // 스킬 3번에 사용될 프리팹
    public float fadeInDuration; // 알파값이 최대로 변하는 시간 (페이드 인)
    public float fadeOutDuration; // 알파값이 다시 원래 값으로 돌아오는 시간 (페이드 아웃)
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        whiteScreen.SetActive(false);
    }

    private void Update()
    {
        KeyInput();
        Move();
        UpdateLayer();

        if(hp < 0)
        {
            StartCoroutine(Die());
        }

        if (keySkilla)
        {
            ThrowGemBomb();
        }

        if (keySkillc && !isJump)
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(WhiteScreenFade());
        }


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
        keyJump   = Input.GetButtonDown("Jump");
        isAttack = Input.GetButton("Attack");
        keySkilla = Input.GetKeyDown(KeyCode.Z);
        keySkillb = Input.GetKeyDown(KeyCode.X);
        keySkillc = Input.GetKeyDown(KeyCode.C);
    }

    private void Move()
    {
        if(!isRope)
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

                if (movedown)
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

        if(isDamage && !isRope)
        {
            gameObject.layer = LayerMask.NameToLayer("PlayerOnDamage");
        }
    }

    public void DecreaseHp(int amount) // 데미지 입는 함수
    {
        if (!isDamage)
        {
            StartCoroutine(OnDamage());
            hp -= amount;
            if (hp < 0)
            {
                hp = 0;
            }
        }
    }

    IEnumerator OnDamage()
    {
        isDamage = true;

        Renderer renderer = GetComponent<Renderer>();
        Material originalMaterial = renderer.material;
        Material blinkMaterial = new Material(originalMaterial);
        blinkMaterial.color = Color.red;

        float blinkDuration = 0.1f; // 깜빡임 간격 (초)
        int blinkCount = 5; // 깜빡임 횟수

        for (int i = 0; i < blinkCount; i++)
        {
            renderer.material = blinkMaterial;
            yield return new WaitForSeconds(blinkDuration);

            renderer.material = originalMaterial;
            yield return new WaitForSeconds(blinkDuration);
        }

        isDamage = false;
        renderer.material = originalMaterial;
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(0f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gem"))
        {
            gemPoint++;
        }
    }


    private void ThrowGemBomb()
    {
        GameObject shotBullet = Instantiate(gemBomb, bulletPosition.position, Quaternion.identity);
        Rigidbody2D rb = shotBullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            float direction = transform.localScale.x > 0 ? 1f : -1f;
            rb.velocity = new Vector2(40f * direction, 0f);

            if (direction < 0) // 왼쪽 방향일 경우 프리펩을 반전시킴
            {
                shotBullet.transform.localScale = new Vector3(-1f, 1f, 1f);  // 반전된 상태로 발사되는 총알의 크기 조절
            }
        }
    }


    private IEnumerator WhiteScreenFade()
    {
        // 페이드 인 (화면이 서서히 밝아짐)
        whiteScreen.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < fadeInDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            SetWhiteScreenAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 알파값이 최대가 될 때 BoxCollider2D 활성화
        whiteScreenDmg.SetActive(true);

        // 0.1초 딜레이 후 BoxCollider2D 비활성화
        yield return new WaitForSeconds(0.1f);
        whiteScreenDmg.SetActive(false);

        // 페이드 아웃 (화면이 서서히 어두워짐)
        elapsedTime = 0f;
        while (elapsedTime < fadeOutDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeOutDuration);
            SetWhiteScreenAlpha(alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 페이드가 끝난 후 화면 가림막 비활성화
        whiteScreen.SetActive(false);
    }

    private void SetWhiteScreenAlpha(float alpha)
    {
        Renderer renderer = whiteScreen.GetComponent<Renderer>();
        Color color = renderer.material.color;
        color.a = alpha;
        renderer.material.color = color;
    }


}
