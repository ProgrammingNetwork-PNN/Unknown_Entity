using UnityEngine;

public class CanvasActivation : MonoBehaviour
{
    public Canvas canvas;

    void Start()
    {
        // 캔버스 컴포넌트 가져오기
        canvas = GetComponent<Canvas>();

        // 33초 후에 ActivateCanvas 메서드 호출
        Invoke("ActivateCanvas", 33.0f);
    }

    void ActivateCanvas()
    {
        // 캔버스 활성화
        canvas.enabled = true;
    }
}
