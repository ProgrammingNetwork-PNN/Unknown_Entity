using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Canvas yourCanvas;
    public float fadeDuration = 2.0f; // 페이드인에 걸리는 시간
    public AudioSource musicAudioSource; // 음악을 재생하는 오디오 소스에 대한 참조

    private Image panelImage;
    private float timer = 0f;

    void Start()
    {
        if (yourCanvas != null)
        {
            // 패널의 이미지 컴포넌트 가져오기
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

        // 일정 시간 이후에 Canvas를 활성화하고 페이드인 효과 시작
        if (!yourCanvas.enabled && timer >= 2.0f)
        {
            yourCanvas.enabled = true;

            // 페이드인 효과를 위한 Coroutine 시작
            StartCoroutine(FadeIn());
        }

        // 음악이 끝나면 게임 종료
        if (!musicAudioSource.isPlaying)
        {
            // 게임 종료 또는 다른 종료 로직을 호출하십시오.
            EndGame();
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            Color currentColor = panelImage.color;
            currentColor.a = alpha;
            panelImage.color = currentColor;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }

    void EndGame()
    {
        // 게임 종료 또는 필요한 종료 로직을 여기에 추가하십시오.
        Application.Quit();
    }
}
