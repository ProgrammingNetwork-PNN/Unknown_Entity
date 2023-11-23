using UnityEngine;

public class SchoolBusMovement : MonoBehaviour
{
    public float speed = 5.0f; // �̵� �ӵ�
    public AudioClip footstepSound; // �߰��� ����
    public AudioClip otherSound; // �ٸ� ���� �߰�
    public float maxFootstepVolume = 1.0f; // �߰��� ���� �ִ� ũ��
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource ������Ʈ�� �������ų� �߰�
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // �߰��� ���� ũ�� �ʱ�ȭ
        audioSource.volume = 0.0f;

        // 24�� �Ŀ� StopMoving �޼��� ȣ��
        Invoke("StopMoving", 24.0f);

        // 1�ʺ��� 24�ʱ��� 0.05�� �����ϸ鼭 PlayFootstepSound �޼��� ȣ��
        for (float t = 1.0f; t <= 22.0f; t += 0.02f)
        {
            Invoke("PlayFootstepSound", t);
        }

        // 26�� �Ŀ� PlayOtherSound �޼��� ȣ��
        Invoke("PlayOtherSound", 25.2f);
    }

    void Update()
    {
        // ������ �̵�
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void PlayFootstepSound()
    {
        // �߰��� ���带 ����ϸ� ũ�⸦ ������Ŵ
        if (!audioSource.isPlaying)
        {
            audioSource.clip = footstepSound;
            audioSource.volume = Mathf.Min(audioSource.volume + 0.05f, maxFootstepVolume);
            audioSource.Play();
        }
    }

    void PlayOtherSound()
    {
        // �ٸ� ���带 ���
        audioSource.clip = otherSound;
        audioSource.Play();
    }

    void StopMoving()
    {
        // �̵��� ���߱� ���� �� ��ũ��Ʈ�� ��Ȱ��ȭ
        enabled = false;
    }
}
