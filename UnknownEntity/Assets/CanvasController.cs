using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    public Canvas yourCanvas;
    public float fadeDuration = 2.0f; // fade-in �ҿ� �ð� ����

    private Image panelImage;
    private float timer = 0f;

    void Start()
    {

        if (yourCanvas != null)
        {
            // Panel�� Image ������Ʈ ��������
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

        // ���� �ð� ���Ŀ� Canvas�� Ȱ��ȭ�ϰ� fade-in ȿ�� ����
        if (!yourCanvas.enabled && timer >= 4.0f)
        {
            yourCanvas.enabled = true;

            // fade-in ȿ���� ���� Coroutine ����
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn()
    {
        // ���� �ð�
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // ���� �� ����
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);

            // Color�� �����Ͽ� ������ ����
            Color currentColor = panelImage.color;
            currentColor.a = alpha;
            panelImage.color = currentColor;

            // �ð� ������Ʈ
            elapsedTime += Time.deltaTime;

            // �� ������ ���
            yield return null;
        }
    }
}
