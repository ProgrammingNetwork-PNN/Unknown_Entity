using UnityEngine;

public class KeyCrad : MonoBehaviour
{
    // �ٸ� �ڵ�� �Բ� ���� ���Դϴ�.
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
    void OnTriggerEnter(Collider other) //�浹ó��
    {
        if(other.CompareTag("Player"))
        {
            key.SetActive(false);
        }
    }
}