using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform player; // �÷��̾� ������Ʈ�� Transform ������Ʈ�� ������ ����
    public float moveSpeed = 5f; // ������ �̵� �ӵ�

    // �� ��� ����
    public float minX = -10f; // �� ���� ���
    public float maxX = 10f; // �� ������ ���
    public float minY = -10f; // �� �Ʒ��� ���
    public float maxY = 10f; // �� ���� ���

    private void FixedUpdate()
    {
        if (player != null)
        {
            // ���Ϳ� �÷��̾� ���� ���� ���� ���
            Vector3 targetPosition = player.position;
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

            // ������ ���� ��ġ���� ��ǥ ��ġ���� �ε巴�� �̵���Ŵ
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        }
    }
}
