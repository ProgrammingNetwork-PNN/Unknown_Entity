using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 올라가는 속도
    public float totalTime = 10.0f; // 총 시간

    private Image imageComponent;
    private float elapsedTime = 0.0f;
    private RectTransform rectTransform;
    private AudioSource audioSource;

    void Start()
    {
        imageComponent = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        audioSource = gameObject.AddComponent<AudioSource>();

        // 10초 동안 MoveImage 코루틴을 시작합니다.
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

        // 이동이 끝난 후에 이미지를 비활성화하거나 다른 작업을 수행할 수 있습니다.
        gameObject.SetActive(false);
    }
}
