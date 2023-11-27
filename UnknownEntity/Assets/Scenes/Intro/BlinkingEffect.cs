using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingEffect : MonoBehaviour
{
    private float originalPosition;
    private bool isBlinking = false;

    void Start()
    {
        // �ʱ� ��ġ ����
        originalPosition = transform.position.z;

        // 2�ʸ��� Blink �Լ� ȣ��
        InvokeRepeating("Blink", 0f, 2f);
    }

    void Blink()
    {
        // �����Ÿ��� ȿ���� �ֱ� ���� Z�� �� ����
        if (isBlinking)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, originalPosition);
        }
        else
        {
            // ���⼭�� Z�� ���� �����Ͽ����� �ٸ� �Ӽ��� ���� ����
            transform.position = new Vector3(transform.position.x, transform.position.y, originalPosition + 1f);
        }

        // �����Ÿ� ���� ����
        isBlinking = !isBlinking;
    }
}
