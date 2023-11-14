using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float lookSpeed = 2f;
    public float upperLookLimit = 80f;
    public float lowerLookLimit = 80f;

    private Rigidbody rb;
    private Animator anim;
    private bool isWalking = false;
    private bool isRunning = false;
    private float rotationX = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Cursor.lockState = CursorLockMode.Locked;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(0f, 0f, verticalInput).normalized;
        Vector3 moveDirection = transform.TransformDirection(movement);

        bool isShiftPressed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        isRunning = isShiftPressed && verticalInput > 0.1f;

        anim.SetBool("isWalking", verticalInput > 0.1f);
        anim.SetBool("isRunning", isRunning);

        float moveSpeed = isRunning ? runSpeed : (isWalking ? walkSpeed : 0f);
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);

        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y += mouseX;
        transform.localRotation = Quaternion.Euler(currentRotation);

        Camera.main.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

        // 디버깅을 위해 콘솔에 값 출력
        UnityEngine.Debug.Log("Vertical Input: " + verticalInput);
        UnityEngine.Debug.Log("Move Speed: " + moveSpeed);
    }
}
