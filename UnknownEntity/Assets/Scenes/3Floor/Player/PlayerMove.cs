using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float lookSpeed = 2f;
    public float upperLookLimit = 60f;
    public float lowerLookLimit = 60f;
    

    public AudioClip footstepsSound;  // 추가: 걷는 소리 클립
    private AudioSource audioSource;  // 추가: AudioSource

    private Rigidbody rb;
    private Animator anim;
    private float rotationX = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();

        // 추가: AudioSource 초기화
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.volume = 0.5f;  // 원하는 볼륨 값으로 조절
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isBack", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", false);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("isBack", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", false);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isBack", false);
            anim.SetBool("isLeft", true);
            anim.SetBool("isRight", false);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isBack", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isBack", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", false);
        }

        // 이동 속도 일정하게 유지
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // 대각선 이동 속도 일정하게 유지
        if (movement.magnitude > 1f)
            movement.Normalize();

        Vector3 moveDirection = transform.TransformDirection(movement);
        rb.MovePosition(rb.position + moveDirection * walkSpeed * Time.deltaTime);
        rb.velocity = moveDirection * walkSpeed;

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);

        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y += mouseX;
        transform.localRotation = Quaternion.Euler(currentRotation);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // 추가: 사운드 재생
        if (movement.magnitude > 0.1f && !audioSource.isPlaying)
        {
            // 추가: 걷는 소리 재생
            audioSource.clip = footstepsSound;
            audioSource.Play();
        }
        else if (movement.magnitude < 0.1f && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
  


}