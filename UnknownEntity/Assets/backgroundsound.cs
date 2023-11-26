using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // ��������� ������ AudioClip ����
    public AudioClip backgroundMusic;

    // AudioSource ������Ʈ
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource ������Ʈ �߰�
        audioSource = gameObject.AddComponent<AudioSource>();

        // AudioClip�� AudioSource�� �Ҵ�
        audioSource.clip = backgroundMusic;

        // ���� ��� ���� (��������� �ݺ��ؼ� ����Ϸ��� true�� ����)
        audioSource.loop = true;

        // AudioSource ���
        audioSource.Play();
    }
}
