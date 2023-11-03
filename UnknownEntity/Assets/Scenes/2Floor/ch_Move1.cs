using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch_Move1 : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float jumpForce = 10f; // 점프 힘
    public float lookSpeed = 2f; // 마우스로 회전하는 속도
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // 캐릭터의 회전을 고정
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 숨기기
    }

    // Update is called once per frame
    void Update()
    {
        // 키 입력 받기
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 이동 방향 계산
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 moveDirection = transform.TransformDirection(movement);

        // 이동하기
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

        // 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;

        // 캐릭터 시선을 마우스 움직임에 맞춰 회전하기
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y += mouseX;
        transform.localRotation = Quaternion.Euler(currentRotation);

        // 점프하기
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
