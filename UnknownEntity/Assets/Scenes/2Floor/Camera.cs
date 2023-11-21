using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform playerTransform;
    public float cameraHeight = 1.2f; // 카메라를 플레이어 위치 위로 올릴 높이 값

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            // 플레이어 위치에 높이 값을 더하여 카메라의 위치로 설정
            transform.position = new Vector3(playerTransform.position.x * 1.0f, playerTransform.position.y * cameraHeight, playerTransform.position.z);

            // 캐릭터의 회전값을 카메라에 적용 (Y 축 회전만 적용하려면 Quaternion.Euler(0f, playerTransform.eulerAngles.y, 0f)을 사용할 수 있습니다)
            transform.rotation = playerTransform.rotation;
        }
        else
        {
            Debug.LogWarning("플레이어 Transform을 찾을 수 없습니다!");
        }
    }
}
