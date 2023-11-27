using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ch_Move1 : MonoBehaviour
{
    public float walkSpeed = 5f; // �⺻ �̵� �ӵ�
    public float runSpeed = 10f; // �޸��� �ӵ�
    public float lookSpeed = 2f; // ���콺�� ȸ���ϴ� �ӵ�
    private Rigidbody rb;
    private bool get_key = false; //Ű ���� ���� ������ false
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
        // Shift Ű�� ���ȴ��� Ȯ���Ͽ� �̵� �ӵ� ����
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? runSpeed : walkSpeed;

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
    }

    void OnTriggerEnter(Collider other) //�浹ó��
    {
        if (other.CompareTag("Exit")) //�浹�� ������Ʈ�� �ױ� ��
        {
            if(get_key==true)
            {
                SceneManager.LoadScene("SceneLoad"); //Base������ �̵�
            }
        }

        else if(other.CompareTag("key"))
        {
            get_key = true;
        }
    }

    public void LoadScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }
}
