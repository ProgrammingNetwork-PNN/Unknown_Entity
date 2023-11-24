using UnityEngine;

public class MoveObject : MonoBehaviour
{
    // �̵��� �ӵ�
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 30.0f; // �߰��� ȸ�� �ӵ�
    public float xRotationTarget = -20.0f; // ��ǥ ȸ�� X ��
    public float xRotationReturnSpeed = 6.0f; // �ǵ����� �ӵ�

    private bool isMoving = true;
    private bool isRotationDone = false;

    void Update()
    {
        if (isMoving)
        {
            // ������Ʈ�� ������ �̵�
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            // 10�� �Ŀ� ��ũ��Ʈ ��Ȱ��ȭ
            Invoke("DisableMovement", 10.0f);
        }
        else if (!isRotationDone)
        {
            // ȸ��
            float rotationStep = rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationStep);

            // ȸ���� �Ϸ�Ǹ� X ȸ������ -20���� �����ϰ� 3�ʰ� ����
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
        // �̵� ���� �� ȸ�� ����
        isMoving = false;
    }

    void ReturnToZeroXRotation()
    {
        // 1�� ���� X ȸ������ 0���� �ǵ�����
        float newXRotation = Mathf.MoveTowards(transform.rotation.eulerAngles.x, 0.0f, xRotationReturnSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(newXRotation, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

        // �ǵ����Ⱑ �Ϸ�Ǹ� ��ũ��Ʈ ��Ȱ��ȭ
        if (newXRotation == 0.0f)
        {
            DisableScript();
        }
    }

    void DisableScript()
    {
        // ��ũ��Ʈ ��Ȱ��ȭ
        enabled = false;
    }
}
