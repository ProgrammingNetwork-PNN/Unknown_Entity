using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraHeight = 1.2f; // ī�޶� �÷��̾� ��ġ ���� �ø� ���� ��

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            // �÷��̾� ��ġ�� ���� ���� ���Ͽ� ī�޶��� ��ġ�� ����
            transform.position = new Vector3(playerTransform.position.x * 1.0f, playerTransform.position.y * cameraHeight, playerTransform.position.z);

            // ĳ������ ȸ������ ī�޶� ���� (Y �� ȸ���� �����Ϸ��� Quaternion.Euler(0f, playerTransform.eulerAngles.y, 0f)�� ����� �� �ֽ��ϴ�)
            transform.rotation = playerTransform.rotation;
        }
        else
        {
            Debug.LogWarning("�÷��̾� Transform�� ã�� �� �����ϴ�!");
        }
    }
}
