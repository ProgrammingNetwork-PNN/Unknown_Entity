using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ch_Move1 : MonoBehaviour
{
    public float walkSpeed = 5f; // 기본 이동 속도
    public float runSpeed = 10f; // 달리기 속도
    public float lookSpeed = 2f; // 마우스로 회전하는 속도
    private Rigidbody rb;
    private bool get_key = false; //키 보유 여부 시작은 false
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
        // Shift 키가 눌렸는지 확인하여 이동 속도 결정
        float moveSpeed = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? runSpeed : walkSpeed;

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
    }

    void OnTriggerEnter(Collider other) //충돌처리
    {
        if (other.CompareTag("Exit")) //충돌한 오브젝트의 테그 비교
        {
            if(get_key==true)
            {
                SceneManager.LoadScene("SceneLoad"); //Base씬으로 이동
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
