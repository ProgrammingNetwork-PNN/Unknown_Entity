using UnityEngine;

public class SchoolBusMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public AudioClip footstepSound;
    public AudioClip stoppingSound; // 새로운 멈추는 소리
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

        // 1초부터 24초까지 발걸음 소리 재생
        for (float t = 1.0f; t <= 22.0f; t += 0.02f)
        {
            Invoke("PlayFootstepSound", t);
        }

        // 24초에 멈추는 소리 재생
        Invoke("PlayStoppingSound", 24.0f);

        // 25.2초에 다른 소리 재생
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
        // 멈추는 소리 재생
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
