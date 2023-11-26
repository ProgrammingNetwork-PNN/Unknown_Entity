using UnityEngine;

public class SchoolBusMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public AudioClip footstepSound;
    public AudioClip stoppingSound; // ���ο� ���ߴ� �Ҹ�
    public AudioClip otherSound;
    public float maxFootstepVolume = 1.0f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.volume = 0.0f;

        Invoke("StopMoving", 24.0f);

        // 1�ʺ��� 24�ʱ��� �߰��� �Ҹ� ���
        for (float t = 1.0f; t <= 22.0f; t += 0.02f)
        {
            Invoke("PlayFootstepSound", t);
        }

        // 24�ʿ� ���ߴ� �Ҹ� ���
        Invoke("PlayStoppingSound", 24.0f);

        // 25.2�ʿ� �ٸ� �Ҹ� ���
        Invoke("PlayOtherSound", 25.2f);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void PlayFootstepSound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = footstepSound;
            audioSource.volume = Mathf.Min(audioSource.volume + 0.05f, maxFootstepVolume);
            audioSource.Play();
        }
    }

    void PlayStoppingSound()
    {
        // ���ߴ� �Ҹ� ���
        audioSource.clip = stoppingSound;
        audioSource.Play();
    }

    void PlayOtherSound()
    {
        audioSource.clip = otherSound;
        audioSource.Play();
    }

    void StopMoving()
    {
        enabled = false;
    }
}
