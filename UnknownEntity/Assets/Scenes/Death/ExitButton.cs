using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    void Start()
    {
        // ��ư�� Ŭ�� �̺�Ʈ ������ �߰�
        Button exitButton = GetComponent<Button>();

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(OnExitButtonClick);
        }
    }

    // ���� ���� �Լ�
    public void OnExitButtonClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        UnityEngine.Application.Quit();
#endif
    }
}
