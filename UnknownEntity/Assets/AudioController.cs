using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip soundClip;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySound();
        StartCoroutine(StopSoundAfterDelay(36f));
    }

    void PlaySound()
    {
        if (soundClip != null)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }
    }

    IEnumerator StopSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopSound();
    }

    void StopSound()
    {
        audioSource.Stop();
    }
}
