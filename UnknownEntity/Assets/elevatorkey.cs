using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class elevatorkey : MonoBehaviour
{
    private bool get_key = false;
    public AudioClip keySound;  // 추가: 키를 먹을 때 재생할 소리
    private AudioSource audioSource;  // 추가: AudioSource

    void Start()
    {
        // 추가: AudioSource 초기화
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = keySound;
        audioSource.volume = 0.5f;  // 원하는 볼륨 값으로 조절
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            if (get_key)
            {
                PlayKeySound();  // 키 소리 재생

                SceneManager.LoadScene("SceneLoad");
            }
        }
        else if (other.CompareTag("key"))
        {
            get_key = true;

            // 키를 먹었을 때 바로 소리 재생
            PlayKeySound();
        }
    }

    void PlayKeySound()
    {
        // 사운드가 재생 중이 아니라면 재생
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void LoadScene(string sceneName)
    {
        PlayKeySound();  // 키 소리 재생

        SceneManager.LoadScene(sceneName);
    }
}