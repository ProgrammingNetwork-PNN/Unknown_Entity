using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 올라가는 속도
    public float totalTime = 10.0f; // 총 시간
    public AudioClip moveSound; // 이동 중 재생할 사운드
    public float soundVolume = 0.5f; // 사운드 볼륨

    private Text textComponent;
    private float elapsedTime = 0.0f;
    private RectTransform rectTransform;
    private AudioSource audioSource;

    void Start()
    {
        textComponent = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
        audioSource = gameObject.AddComponent<AudioSource>();

        // 10초 동안 MoveText 코루틴을 시작합니다.
        StartCoroutine(MoveText());
    }

    IEnumerator MoveText()
    {
        Vector3 startPos = rectTransform.position;
        Vector3 endPos = startPos + Vector3.up * moveSpeed * totalTime;

        // 사운드를 설정하고 재생합니다.
        if (moveSound != null)
        {
            audioSource.clip = moveSound;
            audioSource.volume = soundVolume;
            audioSource.Play();
        }

        while (elapsedTime < totalTime)
        {
            rectTransform.position = Vector3.Lerp(startPos, endPos, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 이동이 끝난 후에 텍스트를 비활성화하거나 다른 작업을 수행할 수 있습니다.
        gameObject.SetActive(false);
    }
}
