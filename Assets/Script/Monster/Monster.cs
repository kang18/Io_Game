using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int hp;
    public int touchdmg;
    public float speed = 5f;
    public bool dodie; // 죽었는지 살았는지
    public bool id; // 프리팹이 왼쪽과 오른쪽 중 어느 방향을 바라보고 있는지에 따라 스프라이트를 뒤집기 위해서
    
    // 피격시 깜빡임을 위한 변수들
    public Color hitColor; // 피격시 보여지는 색상
    public float hitDuration = 0.1f; // 색상이 바뀌는 시간
    private Renderer renderer;
    private Color originalColor;

    public SpawnManager spawnManager; // SpawnManager 스크립트의 참조를 저장할 변수
    public GameObject gem;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
    }

    void OnEnable()
    {
        renderer = GetComponent<Renderer>();
        originalColor = renderer.material.color;
        if (id) // 스프라이트가 반대로 되어 있는 것들 뒤집는 용
        {
            // 오브젝트의 X 스케일을 반전시킴
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        spawnManager = FindObjectOfType<SpawnManager>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.gameObject.GetComponent<Bullet>();

        if (collision.gameObject.CompareTag("Bullet") && !dodie)
        {
            StartCoroutine(OnDamage());
            hp -= bullet.dmg;
            if (hp < 0)
            {
                hp = 0;
            }
        }

        PlayerBehavior playerBehavior = collision.gameObject.GetComponent<PlayerBehavior>();

        if (playerBehavior != null)
        {
            playerBehavior.DecreaseHp(touchdmg);
        }

    }

    public IEnumerator OnDamage()
    {
        renderer.material.color = hitColor;
        yield return new WaitForSeconds(hitDuration);
        renderer.material.color = originalColor;
    }
 
    private void OnDisable()
    {
        spawnManager.catchMonsters++;
    }
}

