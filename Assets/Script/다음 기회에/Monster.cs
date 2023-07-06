using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int hp;
    public int dmg;
    public float speed = 5f;




    public bool id; // 좌우 반전 때문에 임시로 만든거임 


    void OnEnable()
    {
        if(!id)
        {
            // 오브젝트의 X 스케일을 반전시킴
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
       
    }

    private void Update()
    {
        MoveMonster();
    }

    private void MoveMonster()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // 총알에 닿으면 삭제
        {
            Destroy(gameObject);
        }
    }
}
