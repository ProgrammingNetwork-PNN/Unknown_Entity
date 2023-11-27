using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials1 : MonoBehaviour
{
    public Canvas yourCanvas; // Inspector에서 연결할 Canvas 변수
    private bool isCanvasActive = false;
    int count = 0;

    void Start()
    {
        // "CanvasParent"를 찾아서 "Canvas"를 할당
        Transform canvasTransform = GameObject.Find("CanvasParent").transform.Find("Canvas");
        yourCanvas = canvasTransform.GetComponent<Canvas>();
        
        // 만약 "Canvas"가 Canvas 컴포넌트를 가지고 있지 않다면 오류가 발생할 수 있으므로 예외처리를 추가하면 더 안전합니다.
    }

    void Update()
    {
        // F1 키를 누를 때 Canvas를 활성화
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if(count == 2)
            {
                GameObject.Find("CanvasParent").transform.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
                GameObject.Find("CanvasParent").transform.Find("Canvas").transform.GetChild(1).gameObject.SetActive(true);
                GameObject playerObject = GameObject.Find("Player");
                if (playerObject != null)
                {
                    Transform cameraTransform = playerObject.transform.Find("Camera");
                    if (cameraTransform != null)
                    {
                        ShaderEffect_CorruptedVram shaderEffect = cameraTransform.GetComponent<ShaderEffect_CorruptedVram>();
                        if (shaderEffect != null)
                        {
                            // Disable the script
                            shaderEffect.enabled = true;
                        }
                    }
                }
                GameObject.Find("Sound").transform.GetChild(0).gameObject.SetActive(true);
                Invoke("ChangeBool", 1f);
            }
            if(count == 3)
            {
                if(isCanvasActive == false)
                {
                    GameObject.Find("CanvasParent").transform.GetChild(0).gameObject.SetActive(false);
                    isCanvasActive = !isCanvasActive;
                }
            }
            isCanvasActive = !isCanvasActive;
            // Canvas 상태에 따라 활성화 또는 비활성화
            if (isCanvasActive == true)
            {
                GameObject.Find("CanvasParent").transform.GetChild(0).gameObject.SetActive(isCanvasActive);
            }
            else
            {
                yourCanvas.gameObject.SetActive(isCanvasActive);
                GameObject playerObject = GameObject.Find("Player");
                if (playerObject != null)
                {
                    Transform cameraTransform = playerObject.transform.Find("Camera");
                    if (cameraTransform != null)
                    {
                        ShaderEffect_CorruptedVram shaderEffect = cameraTransform.GetComponent<ShaderEffect_CorruptedVram>();
                        if (shaderEffect != null)
                        {
                            // Disable the script
                            shaderEffect.enabled = false;
                        }
                    }
                }
                count++;
            }
        }
    }
    void ChangeBool()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            Transform cameraTransform = playerObject.transform.Find("Camera");
            if (cameraTransform != null)
            {
                ShaderEffect_CorruptedVram shaderEffect = cameraTransform.GetComponent<ShaderEffect_CorruptedVram>();
                if (shaderEffect != null)
                {
                    // Disable the script
                    shaderEffect.enabled = false;
                }
            }
        }
        GameObject.Find("Sound").transform.GetChild(0).gameObject.SetActive(false);
    }
}
