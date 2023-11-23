using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusControl : MonoBehaviour
{
    public Animator Door1;
    public Animator Door2;
    public Animator StopSign1;
    public Animator StopSign3;
    public AudioClip doorOpenSound; // 문 여는 사운드 추가
    public float doorOpenVolume = 0.5f; // 문 여는 사운드 크기
    private AudioSource audioSource;

    public float OpenCloseSpeed;

    private bool isOpen = false;

    void Start()
    {
        // AudioSource 컴포넌트를 가져오거나 추가
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // 30초 후에 OpenDoors 메서드 호출
        Invoke("OpenDoors", 32.0f);
    }

    void OpenDoors()
    {
        // Open 메서드를 호출하여 문을 엽니다.
        Open(true);

        // 문 여는 사운드 재생
        PlayDoorOpenSound();
    }

    void PlayDoorOpenSound()
    {
        // 문 여는 사운드를 재생
        if (doorOpenSound != null && !audioSource.isPlaying)
        {
            audioSource.clip = doorOpenSound;
            audioSource.volume = doorOpenVolume;
            audioSource.Play();
        }
    }

    public void Open(bool open = true)
    {
        string action = "";
        if (open)
        {
            action = "Open";
        }
        else
        {
            action = "Close";
        }
        Door1.SetTrigger(action);
        Door2.SetTrigger(action);
        StopSign1.SetTrigger(action);
        StopSign3.SetTrigger(action);
        isOpen = !isOpen;
    }

    public void ToggleDoor()
    {
        if (isOpen)
        {
            Open(false);
        }
        else
        {
            Open(true);
        }
    }
}
