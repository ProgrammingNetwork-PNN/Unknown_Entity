using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SceneMoved",2);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SceneMoved()
    {
        SceneManager.LoadScene("Base");
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
