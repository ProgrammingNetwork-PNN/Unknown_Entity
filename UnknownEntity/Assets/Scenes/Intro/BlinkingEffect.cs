using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    private float originalPosition;
    private bool isBlinking = false;

    void Start()
    {
        // 초기 위치 저장
        originalPosition = transform.position.z;

        // 2초마다 Blink 함수 호출
        InvokeRepeating("Blink", 0f, 2f);
    }

    void Blink()
    {
        // 깜빡거리는 효과를 주기 위해 Z축 값 변경
        if (isBlinking)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, originalPosition);
        }
        else
        {
            // 여기서는 Z축 값을 변경하였으나 다른 속성도 변경 가능
            transform.position = new Vector3(transform.position.x, transform.position.y, originalPosition + 1f);
        }

        // 깜빡거림 상태 반전
        isBlinking = !isBlinking;
    }
}
