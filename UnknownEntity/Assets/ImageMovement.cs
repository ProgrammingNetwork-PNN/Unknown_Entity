using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // �ö󰡴� �ӵ�
    public float totalTime = 10.0f; // �� �ð�

    private Image imageComponent;
    private float elapsedTime = 0.0f;
    private RectTransform rectTransform;
    private AudioSource audioSource;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        audioSource = gameObject.AddComponent<AudioSource>();

        // 10�� ���� MoveImage �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(MoveImage());
    }

    IEnumerator MoveImage()
    {
        Vector3 startPos = rectTransform.position;
        Vector3 endPos = startPos + Vector3.up * moveSpeed * totalTime;

        while (elapsedTime < totalTime)
        {
            rectTransform.position = Vector3.Lerp(startPos, endPos, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // �̵��� ���� �Ŀ� �̹����� ��Ȱ��ȭ�ϰų� �ٸ� �۾��� ������ �� �ֽ��ϴ�.
        gameObject.SetActive(false);
    }
}
