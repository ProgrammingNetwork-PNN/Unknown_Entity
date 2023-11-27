using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Canvas yourCanvas;
    public float fadeDuration = 2.0f; // ���̵��ο� �ɸ��� �ð�
    public AudioSource musicAudioSource; // ������ ����ϴ� ����� �ҽ��� ���� ����

    private Image panelImage;
    private float timer = 0f;

    void Start()
    {
        if (yourCanvas != null)
        {
            // �г��� �̹��� ������Ʈ ��������
            panelImage = yourCanvas.GetComponentInChildren<Image>();

            // ���� ���� ���� �� ����
            Color startColor = panelImage.color;
            startColor.a = 0f; // ���� �� ���İ��� 0���� ����
            panelImage.color = startColor;

            // Canvas�� ��Ȱ��ȭ�ϰ� ����
            yourCanvas.enabled = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        // ���� �ð� ���Ŀ� Canvas�� Ȱ��ȭ�ϰ� ���̵��� ȿ�� ����
        if (!yourCanvas.enabled && timer >= 2.0f)
        {
            yourCanvas.enabled = true;

            // ���̵��� ȿ���� ���� Coroutine ����
            StartCoroutine(FadeIn());
        }

        // ������ ������ ���� ����
        if (!musicAudioSource.isPlaying)
        {
            // ���� ���� �Ǵ� �ٸ� ���� ������ ȣ���Ͻʽÿ�.
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
        // ���� ���� �Ǵ� �ʿ��� ���� ������ ���⿡ �߰��Ͻʽÿ�.
        Application.Quit();
    }
}
