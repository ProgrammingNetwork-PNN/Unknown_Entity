using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackOut : MonoBehaviour
{
    private Image imagePanel;

    void Start()
    {
        // 이미지 패널의 Image 컴포넌트 가져오기
        imagePanel = GetComponent<Image>();

        // 처음에 한 번은 ToggleAlpha 함수 호출
        InvokeRepeating("ToggleAlpha", 0f, 5f);
    }

    void ToggleAlpha()
    {
        StartCoroutine(ChangeAlpha());
    }

    IEnumerator ChangeAlpha()
    {
        // 처음 2초 동안 불투명도를 0으로 설정
        imagePanel.color = new Color(imagePanel.color.r, imagePanel.color.g, imagePanel.color.b, 0f);
        yield return new WaitForSeconds(3f);

        // 다음 2초 동안 0.2초 간격으로 랜덤한 불투명도로 설정
        for (float t = 0f; t < 2f; t += 0.2f)
        {
            float randomAlpha = Random.Range(0f, 100f) / 180f;
            imagePanel.color = new Color(imagePanel.color.r, imagePanel.color.g, imagePanel.color.b, randomAlpha);
            yield return new WaitForSeconds(0.2f);
        }
    }
}
