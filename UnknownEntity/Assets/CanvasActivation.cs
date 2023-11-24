using UnityEngine;

public class CanvasActivation : MonoBehaviour
{
    public Canvas canvas;

    void Start()
    {
        // ĵ���� ������Ʈ ��������
        canvas = GetComponent<Canvas>();

        // 33�� �Ŀ� ActivateCanvas �޼��� ȣ��
        Invoke("ActivateCanvas", 33.0f);
    }

    void ActivateCanvas()
    {
        // ĵ���� Ȱ��ȭ
        canvas.enabled = true;
    }
}
