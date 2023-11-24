using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // 배경음악을 저장할 AudioClip 변수
    public AudioClip backgroundMusic;

    // AudioSource 컴포넌트
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource 컴포넌트 추가
        audioSource = gameObject.AddComponent<AudioSource>();

        // AudioClip을 AudioSource에 할당
        audioSource.clip = backgroundMusic;

        // 루프 재생 설정 (배경음악을 반복해서 재생하려면 true로 설정)
        audioSource.loop = true;

        // AudioSource 재생
        audioSource.Play();
    }
}
