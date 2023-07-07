using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public float maxLineLength = 10f;
    public LayerMask collisionMask;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        // 라인의 시작점
        Vector3 lineStartPoint = transform.position;

        // 충돌 감지를 위한 변수 초기화
        RaycastHit2D hit;

        // 콜라이더와 충돌하기 전까지의 거리 계산
        float lineLength = maxLineLength;
        hit = Physics2D.Raycast(lineStartPoint, Vector2.up, maxLineLength, collisionMask);
        if (hit.collider != null)
        {
            lineLength = hit.distance;
        }

        // 라인의 끝점 계산
        Vector3 lineEndPoint = lineStartPoint + Vector3.up * lineLength;

        // 라인 렌더러에 포인트들 설정
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, lineStartPoint);
        lineRenderer.SetPosition(1, lineEndPoint);
    }

}
