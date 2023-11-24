using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f; // �ö󰡴� �ӵ�
    public float totalTime = 10.0f; // �� �ð�
    public AudioClip moveSound; // �̵� �� ����� ����
    public float soundVolume = 0.5f; // ���� ����

    private Text textComponent;
    private float elapsedTime = 0.0f;
    private RectTransform rectTransform;
    private AudioSource audioSource;

    void Start()
    {
        textComponent = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
        audioSource = gameObject.AddComponent<AudioSource>();

        // 10�� ���� MoveText �ڷ�ƾ�� �����մϴ�.
        StartCoroutine(MoveText());
    }

    IEnumerator MoveText()
    {
        Vector3 startPos = rectTransform.position;
        Vector3 endPos = startPos + Vector3.up * moveSpeed * totalTime;

        // ���带 �����ϰ� ����մϴ�.
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

        // �̵��� ���� �Ŀ� �ؽ�Ʈ�� ��Ȱ��ȭ�ϰų� �ٸ� �۾��� ������ �� �ֽ��ϴ�.
        gameObject.SetActive(false);
    }
}
