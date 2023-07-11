using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int hp;
    public int dmg;
    public float speed = 5f;

    public bool dodie; // 죽었는지 살았는지

    public bool id; // 프리팹이 왼쪽과 오른쪽 중 어느 방향을 바라보고 있는지에 따라 스프라이트를 뒤집기 위해서

    void OnEnable()
    {
        if(id) // 스프라이트가 반대로 되어 있는 것들 뒤집는 용
        {
            // 오브젝트의 X 스케일을 반전시킴
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet playerBehavior = collision.gameObject.GetComponent<Bullet>();

        if (collision.gameObject.CompareTag("Bullet") && !dodie) 
        {
            StartCoroutine(OnDamage());
            hp -= playerBehavior.dmg;
            if (hp < 0)
            {
                hp = 0;
            }
        }
    }

    IEnumerator OnDamage()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material originalMaterial = renderer.material;
        Material blinkMaterial = new Material(originalMaterial);
        blinkMaterial.color = Color.yellow;

        float blinkDuration = 0.06f; // 깜빡임 간격 (초)
        int blinkCount = 1; // 깜빡임 횟수

        for (int i = 0; i < blinkCount; i++)
        {
            renderer.material = blinkMaterial;
            yield return new WaitForSeconds(blinkDuration);

            renderer.material = originalMaterial;
            yield return new WaitForSeconds(blinkDuration);
        }
        yield return new WaitForSeconds(0.2f);
        renderer.material = originalMaterial;
    }
}
