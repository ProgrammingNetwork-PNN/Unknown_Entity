using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    public void QuitGameOnClick()
    {
        // 어플리케이션 종료
        Application.Quit();

        // 에디터에서 실행 중인 경우 종료가 작동하지 않을 수 있습니다.
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
