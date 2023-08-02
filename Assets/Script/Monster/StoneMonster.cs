using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMonster : Monster
{
    public float rayLength; // 감지 범위
    public float bulletSpeed; // 총알 날아가는 속도

    public GameObject bombArray; // 폭발 범위


    private void Update()
    {
        if (hp <= 0 && !dodie)
        {
            StartCoroutine(Bomb());
        }
        if (!dodie)
        {
            MoveMonster();
        }
    }

    private void MoveMonster() // 좌측으로 이동하는 함수
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }


    private IEnumerator Bomb()
    {
        dodie = true;

        Renderer renderer = GetComponent<Renderer>();
        Color startColor = renderer.material.color;
        Color targetColor = new Color(1f, 0.5f, 0f);
        float duration = 2f; // 변화에 걸리는 시간
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; // 보간값 계산
            renderer.material.color = Color.Lerp(startColor, targetColor, t); // 보간하여 색상 변경
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        renderer.material.color = targetColor; // 최종 색상 설정
        bombArray.SetActive(true);
       
        yield return new WaitForSeconds(1f);
        if (Random.Range(0f, 100f) <= 30f)
        {
            GameObject newObj = Instantiate(gem, transform.position, Quaternion.identity);
        }

        Destroy(gameObject); // 오브젝트 삭제
    }
}


