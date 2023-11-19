using UnityEngine;

public class KeyCrad : MonoBehaviour
{
    // 다른 코드와 함께 있을 것입니다.
    GameObject key;
    Collider box;
    private void Awake()
    {
        box = GetComponent<Collider>();
    }

    private void Start()
    {
        key = GameObject.Find("key");
    }
    void OnTriggerEnter(Collider other) //충돌처리
    {
        if(other.CompareTag("Player"))
        {
            key.SetActive(false);
        }
    }
}