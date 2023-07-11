using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public int hp;

    public Color hitColor;  // 피격시 보여지는 색상
    public float hitDuration = 0.1f;  // 색상이 바뀌는 시간

    private Renderer objectRenderer;
    private Color originalColor;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    public void TakeDamage(int dmg) // 색상을 변경하는 코루틴 함수 실행
    {
        hp -= dmg;
        StartCoroutine(ChangeColorCoroutine());
    }

    private IEnumerator ChangeColorCoroutine()
    {
        objectRenderer.material.color = hitColor;
        yield return new WaitForSeconds(hitDuration);
        objectRenderer.material.color = originalColor;
    }
}
