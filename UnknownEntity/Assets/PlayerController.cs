using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 3.0f;
    public float rotationSpeed = 30.0f;
    public float lookUpSpeed = 50.0f;
    private float walkTimer = 0.0f;
    private float rotationTimer = 0.0f;
    private float lookUpTimer = 0.0f;
    private float lookDownTimer = 0.0f;
    private bool isWalking = true;
    private bool rotationComplete = false;
    private bool lookUpComplete = false;
    private bool turnBackComplete = false;
    private bool lookDownComplete = false;
    private AudioSource footstepAudio;
    private int rotationSequenceCount = 0; // New variable to track the rotation sequence count

    void Start()
    {
        footstepAudio = GetComponent<AudioSource>();
        if (footstepAudio == null)
        {
            footstepAudio = gameObject.AddComponent<AudioSource>();
        }

        // 시작 시 WalkForFiveSeconds 코루틴을 시작합니다.
        StartCoroutine(WalkForFiveSeconds());
    }

    void Update()
    {
        if (isWalking && !rotationComplete)
        {
            transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            walkTimer += Time.deltaTime;
            PlayFootstepSound();

            if (walkTimer >= 20.0f)
            {
                isWalking = false;
                rotationTimer = 0.0f;
            }
        }
        else if (!rotationComplete && !turnBackComplete)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * rotationAmount);
            rotationTimer += Time.deltaTime;

            if (rotationTimer >= 2.7f)
            {
                rotationComplete = true;
                rotationTimer = 0.0f;
                lookUpTimer = 0.0f;
            }
        }
        else if (!lookUpComplete && !turnBackComplete)
        {
            float lookUpAmount = lookUpSpeed * Time.deltaTime;
            transform.Rotate(Vector3.left * lookUpAmount);
            lookUpTimer += Time.deltaTime;

            if (lookUpTimer >= 3.0f)
            {
                lookUpComplete = true;
                lookUpTimer = 0.0f;
                turnBackComplete = true;
            }
        }
        else if (!lookDownComplete && turnBackComplete && rotationSequenceCount < 1)
        {
            float lookDownAmount = lookUpSpeed * Time.deltaTime;
            transform.Rotate(Vector3.right * lookDownAmount);
            lookDownTimer += Time.deltaTime;

            if (lookDownTimer >= 3.0f)
            {
                lookDownComplete = true;
                lookDownTimer = 0.0f;
                isWalking = true;
                rotationComplete = false;
                turnBackComplete = false;
                lookDownComplete = false;
                rotationSequenceCount++; // Increment the rotation sequence count
            }
        }
        else
        {
            enabled = false;
        }
    }

    void PlayFootstepSound()
    {
        if (!footstepAudio.isPlaying)
        {
            footstepAudio.Play();
        }
    }

    // 추가된 코루틴
    private IEnumerator WalkForFiveSeconds()
    {
        while (true)
        {
            if (isWalking)
            {
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
                PlayFootstepSound();
            }
            yield return null;
        }
    }
}
