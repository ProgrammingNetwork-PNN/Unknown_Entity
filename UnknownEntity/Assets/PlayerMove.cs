using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float runSpeed = 15f;  // ����: �ٴ� �ӵ� ����
    public float lookSpeed = 2f;
    public float upperLookLimit = 60f;
    public float lowerLookLimit = 60f;

    public AudioClip footstepsSound;  // �߰�: �ȴ� �Ҹ� Ŭ��
    public AudioClip runningFootstepsSound;  // �߰�: �ٴ� �Ҹ� Ŭ��
    private AudioSource audioSource;  // �߰�: AudioSource

    private Rigidbody rb;
    private Animator anim;
    private float rotationX = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();

        // �߰�: AudioSource �ʱ�ȭ
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.volume = 0.5f;  // ���ϴ� ���� ������ ����
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Shift Ű �Է� ���ο� ���� isRunning ����
        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool isRunning = isShiftPressed && Mathf.Abs(verticalInput) > 0.1f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (isShiftPressed)
            {
                anim.SetBool("isRunning", true);
                anim.SetBool("isWalking", false);
                anim.SetBool("isBack", false);
                anim.SetBool("isLeft", false);
                anim.SetBool("isRight", false);
            }
            else
            {
                anim.SetBool("isRunning", false);
                anim.SetBool("isWalking", true);
                anim.SetBool("isBack", false);
                anim.SetBool("isLeft", false);
                anim.SetBool("isRight", false);
            }
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isBack", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", false);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isBack", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLeft", true);
            anim.SetBool("isRight", false);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isBack", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isBack", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isLeft", false);
            anim.SetBool("isRight", false);
        }

        // ����, �¿�, �Ĺ����� �̵��ϵ��� ����
        float moveSpeed = isRunning ? runSpeed : walkSpeed;
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        // �밢�� �̵� �ӵ� �����ϰ� ����
        if (movement.magnitude > 1f)
            movement.Normalize();

        Vector3 moveDirection = transform.TransformDirection(movement);
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);
        rb.velocity = moveDirection * moveSpeed;

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);

        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y += mouseX;
        transform.localRotation = Quaternion.Euler(currentRotation);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // �߰�: ���� ���
        if (movement.magnitude > 0.1f && !audioSource.isPlaying)
        {
            if (isRunning)
            {
                // �߰�: �ٴ� �Ҹ� ���
                audioSource.clip = runningFootstepsSound;
            }
            else
            {
                // �߰�: �ȴ� �Ҹ� ���
                audioSource.clip = footstepsSound;
            }

            audioSource.Play();
        }
        else if (movement.magnitude < 0.1f && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}