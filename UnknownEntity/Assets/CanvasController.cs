using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Canvas yourCanvas;
    public float fadeDuration = 2.0f; // fade-in 소요 시간 설정

    private Image panelImage;
    private float timer = 0f;

    void Start()
    {

        if (yourCanvas != null)
        {
            // Panel의 Image 컴포넌트 가져오기
            panelImage = yourCanvas.GetComponentInChildren<Image>();

            // 시작 시의 알파 값 설정
            Color startColor = panelImage.color;
            startColor.a = 0f; // 시작 시 알파값을 0으로 설정
            panelImage.color = startColor;

            // Canvas를 비활성화하고 시작
            yourCanvas.enabled = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 일정 시간 이후에 Canvas를 활성화하고 fade-in 효과 시작
        if (!yourCanvas.enabled && timer >= 4.0f)
        {
            yourCanvas.enabled = true;

            // fade-in 효과를 위한 Coroutine 시작
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        // 현재 시간
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // 알파 값 조절
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            // Color를 설정하여 불투명도 조절
            Color currentColor = panelImage.color;
            currentColor.a = alpha;
            panelImage.color = currentColor;

            // 시간 업데이트
            elapsedTime += Time.deltaTime;

            // 한 프레임 대기
            yield return null;
        }
    }
}
