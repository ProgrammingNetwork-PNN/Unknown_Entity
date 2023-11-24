using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // 이동할 속도
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 30.0f; // 추가된 회전 속도
    public float xRotationTarget = -20.0f; // 목표 회전 X 값
    public float xRotationReturnSpeed = 6.0f; // 되돌리기 속도
    public float xRotationRestoreDelay = 3.0f; // 추가된 회전 이후에 복원 시작할 시간
    public float xRotationRestoreAmount = 20.0f; // 복원할 회전량
    public float yRotationTarget = 90.0f; // 목표 회전 Y 값

    private bool isMoving = true;
    private bool isRotationDone = false;

    void Update()
    {
        if (isMoving)
        {
            // 오브젝트를 앞으로 이동
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // 10초 후에 스크립트 비활성화
            Invoke("DisableMovement", 15.0f);
        }
        else if (!isRotationDone)
        {
            // 회전
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationStep);

            // 회전이 완료되면 X 회전값을 -20으로 설정하고 천천히 이동
            if (transform.rotation.eulerAngles.y >= 270.0f)
            {
                isRotationDone = true;
                StartCoroutine(SmoothReturnToZeroXRotation());
            }
        }
    }

    void DisableMovement()
    {
        // 이동 정지 및 회전 시작
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

        // 마지막에 정확하게 목표로 설정
        transform.rotation = Quaternion.Euler(xRotationTarget, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        // 지연 후 Rotaion X를 0으로 되돌리는 코루틴 시작
        yield return new WaitForSeconds(xRotationRestoreDelay);
        StartCoroutine(SmoothReturnToZeroXRotationX());
    }

    IEnumerator SmoothReturnToZeroXRotationX()
    {
        float elapsed_time = xRotationReturnSpeed;
        float startRotationX = transform.rotation.eulerAngles.x;
        float targetRotationX = xRotationTarget; // 목표 회전 X 값으로 변경

        while (elapsed_time > 0.0f)
        {
            // 여기서 수정
            float newRotationX = Mathf.MoveTowards(startRotationX, targetRotationX, (xRotationReturnSpeed - elapsed_time) / xRotationReturnSpeed);
            transform.rotation = Quaternion.Euler(newRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            elapsed_time -= Time.deltaTime;
            yield return null;
        }

        // 마지막에 정확하게 목표로 설정
        transform.rotation = Quaternion.Euler(targetRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        // 되돌리기가 완료되면 Y 회전값을 90으로 설정하고 천천히 이동
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

        // 마지막에 정확하게 목표로 설정
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotationY, transform.rotation.eulerAngles.z);

        // 회전이 완료되면 스크립트 비활성화
        DisableScript();
    }

    void DisableScript()
    {
        // 스크립트 비활성화
        enabled = false;
    }
}
