using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public Transform player; // 플레이어 오브젝트의 Transform 컴포넌트를 저장할 변수
    public float moveSpeed = 5f; // 몬스터의 이동 속도

    // 맵 경계 설정
    public float minX = -10f; // 맵 왼쪽 경계
    public float maxX = 10f; // 맵 오른쪽 경계
    public float minY = -10f; // 맵 아래쪽 경계
    public float maxY = 10f; // 맵 위쪽 경계

    private void FixedUpdate()
    {
        if (player != null)
        {
            // 몬스터와 플레이어 간의 방향 벡터 계산
            Vector3 targetPosition = player.position;
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

            // 몬스터의 현재 위치에서 목표 위치까지 부드럽게 이동시킴
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        }
    }
}
