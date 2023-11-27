using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 버튼 클릭 시 호출할 메서드
    public void LoadTargetScene()
    {
        // SceneManager를 통해 씬을 로드
        SceneManager.LoadScene("3Base");
    }
}

