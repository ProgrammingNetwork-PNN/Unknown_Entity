using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardBackgroundMusic : MonoBehaviour
{
    public AudioClip backgroundMusic;
    private AudioSource audioSource;
    private bool musicStarted = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
    }

    public void StartBackgroundMusic()
    {
        if (!musicStarted)
        {
            audioSource.Play();
            musicStarted = true;
        }
    }
}