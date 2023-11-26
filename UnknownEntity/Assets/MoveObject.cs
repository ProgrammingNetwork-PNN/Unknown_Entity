using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 30.0f;
    public float xRotationTarget = -20.0f;
    public float xRotationReturnSpeed = 6.0f;
    public float xRotationRestoreDelay = 3.0f;
    public float xRotationRestoreAmount = 20.0f;
    public float yRotationTarget = 90.0f;

    public AudioClip moveSound; // Sound for movement
    public AudioClip rotationSound; // Sound for rotation

    private bool isMoving = true;
    private bool isRotationDone = false;
    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component to the GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (isMoving)
        {
            // Move forward
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // Play move sound
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(moveSound);

            Invoke("DisableMovement", 15.0f);
        }
        else if (!isRotationDone)
        {
            // Rotate
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationStep);

            // Play rotation sound
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(rotationSound);

            if (transform.rotation.eulerAngles.y >= 270.0f)
            {
                isRotationDone = true;
                StartCoroutine(SmoothReturnToZeroXRotation());
            }
        }
    }

    void DisableMovement()
    {
        isMoving = false;
    }

    IEnumerator SmoothReturnToZeroXRotation()
    {
        float elapsed_time = 0.0f;
        float startRotationX = transform.rotation.eulerAngles.x;

        while (elapsed_time < xRotationReturnSpeed)
        {
            float newRotationX = Mathf.Lerp(startRotationX, xRotationTarget, elapsed_time / xRotationReturnSpeed);
            transform.rotation = Quaternion.Euler(newRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            elapsed_time += Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(xRotationTarget, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        yield return new WaitForSeconds(xRotationRestoreDelay);
        StartCoroutine(SmoothReturnToZeroXRotationX());
    }

    IEnumerator SmoothReturnToZeroXRotationX()
    {
        float elapsed_time = xRotationReturnSpeed;
        float startRotationX = transform.rotation.eulerAngles.x;
        float targetRotationX = xRotationTarget;

        while (elapsed_time > 0.0f)
        {
            float newRotationX = Mathf.MoveTowards(startRotationX, targetRotationX, (xRotationReturnSpeed - elapsed_time) / xRotationReturnSpeed);
            transform.rotation = Quaternion.Euler(newRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            elapsed_time -= Time.deltaTime;
            yield return null;
        }

        transform.rotation = Quaternion.Euler(targetRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        StartCoroutine(SmoothRotateToYTarget());
    }

    IEnumerator SmoothRotateToYTarget()
    {
        float startRotationY = transform.rotation.eulerAngles.y;
        float targetRotationY = yRotationTarget;

        while (Mathf.Abs(targetRotationY - transform.rotation.eulerAngles.y) > 0.01f)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotationY, transform.rotation.eulerAngles.z), step);
            yield return null;
        }

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotationY, transform.rotation.eulerAngles.z);
        DisableScript();
    }

    void DisableScript()
    {
        enabled = false;
    }
}
