using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackScrolling : MonoBehaviour
{
    public Transform[] layers; // 패럴랙스 배경 레이어들
    public float[] parallaxFactorsX; // 각 배경 레이어의 좌우 패럴랙스 계수
    public float[] parallaxFactorsY; // 각 배경 레이어의 위아래 패럴랙스 계수
    public Transform playerTransform; // 플레이어의 Transform 컴포넌트

    public float smoothSpeed = 0.1f; // 패럴랙스 배경의 부드러운 이동 정도

    private Vector3[] initialPositions; // 초기 위치 저장
    private Vector3[] targetPositions; // 목표 위치 저장

    private void Start()
    {
        // 초기 위치 저장
        initialPositions = new Vector3[layers.Length];
        targetPositions = new Vector3[layers.Length];
        for (int i = 0; i < layers.Length; i++)
        {
            initialPositions[i] = layers[i].position;
            targetPositions[i] = initialPositions[i];
        }
    }

    private void Update()
    {
        for (int i = 0; i < layers.Length; i++)
        {
            // 패럴랙스 배경의 목표 위치 계산
            float parallaxAmountX = playerTransform.position.x * parallaxFactorsX[i];
            float parallaxAmountY = -playerTransform.position.y * parallaxFactorsY[i]; // 위아래 이동 방향을 반대로 설정
            targetPositions[i] = initialPositions[i] + new Vector3(parallaxAmountX, parallaxAmountY, 0f);
        }

        // 보간을 사용하여 패럴랙스 배경 부드럽게 이동
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].position = Vector3.Lerp(layers[i].position, targetPositions[i], smoothSpeed * Time.deltaTime);
        }
    }
}
