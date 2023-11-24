using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class elevatorkey : MonoBehaviour
{
    private bool get_key = false;
    public AudioClip keySound;  // �߰�: Ű�� ���� �� ����� �Ҹ�
    private AudioSource audioSource;  // �߰�: AudioSource

    void Start()
    {
        // �߰�: AudioSource �ʱ�ȭ
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = keySound;
        audioSource.volume = 0.5f;  // ���ϴ� ���� ������ ����
        audioSource.playOnAwake = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            if (get_key)
            {
                PlayKeySound();  // Ű �Ҹ� ���

                SceneManager.LoadScene("SceneLoad");
            }
        }
        else if (other.CompareTag("key"))
        {
            get_key = true;

            // Ű�� �Ծ��� �� �ٷ� �Ҹ� ���
            PlayKeySound();
        }
    }

    void PlayKeySound()
    {
        // ���尡 ��� ���� �ƴ϶�� ���
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void LoadScene(string sceneName)
    {
        PlayKeySound();  // Ű �Ҹ� ���

        SceneManager.LoadScene(sceneName);
    }
}