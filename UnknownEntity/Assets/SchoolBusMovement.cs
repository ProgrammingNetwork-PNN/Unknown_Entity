using UnityEngine;

public class SchoolBusMovement : MonoBehaviour
{
    public float speed = 5.0f; // 이동 속도
    public AudioClip footstepSound; // 발걸음 사운드
    public AudioClip otherSound; // 다른 사운드 추가
    public float maxFootstepVolume = 1.0f; // 발걸음 사운드 최대 크기
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource 컴포넌트를 가져오거나 추가
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 발걸음 사운드 크기 초기화
        audioSource.volume = 0.0f;

        // 24초 후에 StopMoving 메서드 호출
        Invoke("StopMoving", 24.0f);

        // 1초부터 24초까지 0.05씩 증가하면서 PlayFootstepSound 메서드 호출
        for (float t = 1.0f; t <= 22.0f; t += 0.02f)
        {
            Invoke("PlayFootstepSound", t);
        }

        // 26초 후에 PlayOtherSound 메서드 호출
        Invoke("PlayOtherSound", 25.2f);
    }

    void Update()
    {
        // 앞으로 이동
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void PlayFootstepSound()
    {
        // 발걸음 사운드를 재생하며 크기를 증가시킴
        if (!audioSource.isPlaying)
        {
            audioSource.clip = footstepSound;
            audioSource.volume = Mathf.Min(audioSource.volume + 0.05f, maxFootstepVolume);
            audioSource.Play();
        }
    }

    void PlayOtherSound()
    {
        // 다른 사운드를 재생
        audioSource.clip = otherSound;
        audioSource.Play();
    }

    void StopMoving()
    {
        // 이동을 멈추기 위해 이 스크립트를 비활성화
        enabled = false;
    }
}
