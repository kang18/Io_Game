using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackScrolling : MonoBehaviour
{
    [Header("이미지 파일")]
    public Transform[] layers; // 패럴랙스 배경 레이어들

    [Header("배경 이동속도")]
    public float[] parallaxFactorsX; // 각 배경 레이어의 좌우 패럴랙스 계수
    public float[] parallaxFactorsY; // 각 배경 레이어의 위아래 패럴랙스 계수


    public Transform playerTransform; // 플레이어의 Transform 컴포넌트

    public float minY; // Y축 이동의 하한
    public float maxY; // Y축 이동의 상한

    public float smoothSpeed; // 패럴랙스 배경의 부드러운 이동 정도

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
            float parallaxAmountX = playerTransform.position.x * parallaxFactorsX[i] * (-1f);
            float parallaxAmountY = Mathf.Clamp(-playerTransform.position.y * parallaxFactorsY[i], -maxY, -minY); // 위아래 이동 방향을 반대로 설정하고 Y축 이동 범위를 제한
            targetPositions[i] = initialPositions[i] + new Vector3(parallaxAmountX, parallaxAmountY, 0f);
        }

        // 보간을 사용하여 패럴랙스 배경 부드럽게 이동
        for (int i = 0; i < layers.Length; i++)
        {
            layers[i].position = Vector3.Lerp(layers[i].position, targetPositions[i], smoothSpeed * Time.deltaTime);
        }
    }
}
