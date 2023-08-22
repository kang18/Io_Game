using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public float fadeDuration;

    private SpriteRenderer spriteRenderer;
    private Color startColor;
    private bool fading = false;

    // 수정된 부분: 생성 시 바로 FadeAndDestroyRoutine()를 호출하여 페이드아웃 시작
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = spriteRenderer.color;

        // 시작하자마자 페이드아웃 및 삭제 루틴 시작
        StartCoroutine(FadeAndDestroyRoutine());
    }

    private IEnumerator FadeAndDestroyRoutine()
    {
        yield return new WaitForSeconds(1.0f);

        fading = true;
        float elapsedTime = 0f;
        float startAlpha = spriteRenderer.color.a; // 현재 알파값 저장

        while (elapsedTime < fadeDuration)
        {
            float normalizedTime = elapsedTime / fadeDuration;
            float alpha = Mathf.Lerp(startAlpha, 0f, normalizedTime); // 시작 알파값으로부터 페이드아웃 시작
            alpha = Mathf.Max(alpha, 0f); // 알파값이 0 미만이 되지 않도록 보장

            Color newColor = new Color(startColor.r, startColor.g, startColor.b, alpha);
            spriteRenderer.color = newColor;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }




    private void Update()
    {
        // 화면을 떠난 경우 삭제
        if (!fading && !spriteRenderer.isVisible)
        {
            Destroy(gameObject);
        }
    }
}