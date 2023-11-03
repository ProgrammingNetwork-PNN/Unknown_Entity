using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ch_Move1 : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float jumpForce = 10f; // ���� ��
    public float lookSpeed = 2f; // ���콺�� ȸ���ϴ� �ӵ�
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // ĳ������ ȸ���� ����
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� �����
    }

    // Update is called once per frame
    void Update()
    {
        // Ű �Է� �ޱ�
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // �̵� ���� ���
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput).normalized;
        Vector3 moveDirection = transform.TransformDirection(movement);

        // �̵��ϱ�
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.deltaTime);

        // ���콺 �Է� �ޱ�
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;

        // ĳ���� �ü��� ���콺 �����ӿ� ���� ȸ���ϱ�
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        currentRotation.y += mouseX;
        transform.localRotation = Quaternion.Euler(currentRotation);

        // �����ϱ�
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
