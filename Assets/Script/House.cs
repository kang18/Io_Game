using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    public Color hitColor;  // 피격시 보여지는 색상
    public float hitDuration = 0.1f;  // 색상이 바뀌는 시간

    private Renderer objectRenderer;
    private Color originalColor;

    public PlayerBehavior player;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
    }

    public void TakeDamage(int dmg) // 색상을 변경하는 코루틴 함수 실행
    {
        if(player.hp > 0)
        {
            player.hp -= dmg;
        }
        
        StartCoroutine(ChangeColorCoroutine());
    }

    private IEnumerator ChangeColorCoroutine()
    {
        objectRenderer.material.color = hitColor;
        yield return new WaitForSeconds(hitDuration);
        objectRenderer.material.color = originalColor;
    }
}
