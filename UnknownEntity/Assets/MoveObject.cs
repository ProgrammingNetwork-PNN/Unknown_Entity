using System.Collections;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // �̵��� �ӵ�
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 30.0f; // �߰��� ȸ�� �ӵ�
    public float xRotationTarget = -20.0f; // ��ǥ ȸ�� X ��
    public float xRotationReturnSpeed = 6.0f; // �ǵ����� �ӵ�
    public float xRotationRestoreDelay = 3.0f; // �߰��� ȸ�� ���Ŀ� ���� ������ �ð�
    public float xRotationRestoreAmount = 20.0f; // ������ ȸ����
    public float yRotationTarget = 90.0f; // ��ǥ ȸ�� Y ��

    private bool isMoving = true;
    private bool isRotationDone = false;

    void Update()
    {
        if (isMoving)
        {
            // ������Ʈ�� ������ �̵�
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // 10�� �Ŀ� ��ũ��Ʈ ��Ȱ��ȭ
            Invoke("DisableMovement", 15.0f);
        }
        else if (!isRotationDone)
        {
            // ȸ��
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationStep);

            // ȸ���� �Ϸ�Ǹ� X ȸ������ -20���� �����ϰ� õõ�� �̵�
            if (transform.rotation.eulerAngles.y >= 270.0f)
            {
                isRotationDone = true;
                StartCoroutine(SmoothReturnToZeroXRotation());
            }
        }
    }

    void DisableMovement()
    {
        // �̵� ���� �� ȸ�� ����
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

        // �������� ��Ȯ�ϰ� ��ǥ�� ����
        transform.rotation = Quaternion.Euler(xRotationTarget, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        // ���� �� Rotaion X�� 0���� �ǵ����� �ڷ�ƾ ����
        yield return new WaitForSeconds(xRotationRestoreDelay);
        StartCoroutine(SmoothReturnToZeroXRotationX());
    }

    IEnumerator SmoothReturnToZeroXRotationX()
    {
        float elapsed_time = xRotationReturnSpeed;
        float startRotationX = transform.rotation.eulerAngles.x;
        float targetRotationX = xRotationTarget; // ��ǥ ȸ�� X ������ ����

        while (elapsed_time > 0.0f)
        {
            // ���⼭ ����
            float newRotationX = Mathf.MoveTowards(startRotationX, targetRotationX, (xRotationReturnSpeed - elapsed_time) / xRotationReturnSpeed);
            transform.rotation = Quaternion.Euler(newRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            elapsed_time -= Time.deltaTime;
            yield return null;
        }

        // �������� ��Ȯ�ϰ� ��ǥ�� ����
        transform.rotation = Quaternion.Euler(targetRotationX, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        // �ǵ����Ⱑ �Ϸ�Ǹ� Y ȸ������ 90���� �����ϰ� õõ�� �̵�
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

        // �������� ��Ȯ�ϰ� ��ǥ�� ����
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotationY, transform.rotation.eulerAngles.z);

        // ȸ���� �Ϸ�Ǹ� ��ũ��Ʈ ��Ȱ��ȭ
        DisableScript();
    }

    void DisableScript()
    {
        // ��ũ��Ʈ ��Ȱ��ȭ
        enabled = false;
    }
}
