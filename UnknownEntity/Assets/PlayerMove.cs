using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 10f;
    public float lookSpeed = 2f;
    public float upperLookLimit = 60f;
    public float lowerLookLimit = 60f;
    public AudioClip footstepsSound;
    private AudioSource audioSource;
    private Rigidbody rb;
    private Animator anim;
    private float rotationX = 0;

    private bool isDeathScene = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        anim = GetComponent<Animator>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = false;
        audioSource.volume = 0.5f;
        audioSource.playOnAwake = false;

        // 초기에 마우스 커서를 비활성화
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (isDeathScene)
        {
            // Death 씬이면 마우스 커서를 활성화
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;  // 여기서 Update 종료
        }

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

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if (movement.magnitude > 1f)
            movement.Normalize();

        Vector3 moveDirection = transform.TransformDirection(movement);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, walkSpeed * Time.deltaTime))
        {
            rb.MovePosition(rb.position + moveDirection.normalized * hit.distance);
        }
        else
        {
            rb.MovePosition(rb.position + moveDirection * walkSpeed * Time.deltaTime);
        }

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);

        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y += mouseX;
        transform.localRotation = Quaternion.Euler(currentRotation);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        if (movement.magnitude > 0.1f && !audioSource.isPlaying)
        {
            audioSource.clip = footstepsSound;
            audioSource.Play();
        }
        else if (movement.magnitude < 0.1f && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Monster"))
        {
            SceneManager.LoadScene("Death");
            isDeathScene = true;
        }
    }
}
