using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    Vector3 cameraPosition;

    [SerializeField]
    Vector2 center;
    [SerializeField]
    Vector2 mapSize;

    [SerializeField]
    float cameraMoveSpeed;
    float height;
    float width;

    float lx;
    float ly;

    float clampX;
    float clampY;

    Vector3 temp;

    void Start()
    {
        LimitCameraArea();   // 게임이 시작한 직후에도 목표를 잘 잡을 수 있도록
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    void LateUpdate()
    {
        LimitCameraArea();
    }

    void LimitCameraArea()
    {
        transform.position = Vector3.Lerp(transform.position,
                                          playerTransform.position + cameraPosition,
                                          Time.deltaTime * cameraMoveSpeed);
        lx = mapSize.x - width;
        clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        ly = mapSize.y - height;
        clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y);
        temp.x = clampX;
        temp.y = clampY;
        temp.z = -10f;

        transform.position = temp;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapSize * 2);
    }

}
