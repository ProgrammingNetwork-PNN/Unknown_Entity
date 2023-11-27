using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOut : MonoBehaviour
{
    private Image imagePanel;

    void Start()
    {
        // �̹��� �г��� Image ������Ʈ ��������
        imagePanel = GetComponent<Image>();

        // ó���� �� ���� ToggleAlpha �Լ� ȣ��
        InvokeRepeating("ToggleAlpha", 0f, 5f);
    }

    void ToggleAlpha()
    {
        StartCoroutine(ChangeAlpha());
    }

    IEnumerator ChangeAlpha()
    {
        // ó�� 2�� ���� �������� 0���� ����
        imagePanel.color = new Color(imagePanel.color.r, imagePanel.color.g, imagePanel.color.b, 0f);
        yield return new WaitForSeconds(3f);

        // ���� 2�� ���� 0.2�� �������� ������ �������� ����
        for (float t = 0f; t < 2f; t += 0.2f)
        {
            float randomAlpha = Random.Range(0f, 100f) / 180f;
            imagePanel.color = new Color(imagePanel.color.r, imagePanel.color.g, imagePanel.color.b, randomAlpha);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
