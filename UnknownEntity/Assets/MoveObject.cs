using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // 이동할 속도
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 30.0f; // 추가된 회전 속도
    public float xRotationTarget = -20.0f; // 목표 회전 X 값
    public float xRotationReturnSpeed = 6.0f; // 되돌리기 속도

    private bool isMoving = true;
    private bool isRotationDone = false;

    void Update()
    {
        if (isMoving)
        {
            // 오브젝트를 앞으로 이동
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // 10초 후에 스크립트 비활성화
            Invoke("DisableMovement", 10.0f);
        }
        else if (!isRotationDone)
        {
            // 회전
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationStep);

            // 회전이 완료되면 X 회전값을 -20으로 설정하고 3초간 유지
            if (transform.rotation.eulerAngles.y >= 270.0f)
            {
                isRotationDone = true;
                transform.rotation = Quaternion.Euler(xRotationTarget, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
                Invoke("ReturnToZeroXRotation", 3.0f);
            }
        }
    }

    void DisableMovement()
    {
        // 이동 정지 및 회전 시작
        isMoving = false;
    }

    void ReturnToZeroXRotation()
    {
        // 1초 동안 X 회전값을 0으로 되돌리기
        float newXRotation = Mathf.MoveTowards(transform.rotation.eulerAngles.x, 0.0f, xRotationReturnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(newXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        // 되돌리기가 완료되면 스크립트 비활성화
        if (newXRotation == 0.0f)
        {
            DisableScript();
        }
    }

    void DisableScript()
    {
        // 스크립트 비활성화
        enabled = false;
    }
}
