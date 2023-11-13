using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip walkingSound;
    private AudioSource audioSource;
    private bool isWalking;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isWalking)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(walkingSound);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("¹Ù´Ú"))
        {
            isWalking = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("¹Ù´Ú"))
        {
            isWalking = false;
        }
    }
}

