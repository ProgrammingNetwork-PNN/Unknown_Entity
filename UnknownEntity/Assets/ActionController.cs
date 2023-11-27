using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    public GameObject mon;
    [SerializeField]
    private float range;
    public bool get_key = false;
    private bool pickupActivated = false;
    private bool pressed_button = false;
    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private Text actionText;

    [SerializeField]
    private AudioSource pickupSound;

    [SerializeField]
    private KeycardBackgroundMusic keycardBackgroundMusicScript; // KeycardBackgroundMusic 스크립트를 참조할 변수

    void Update()
    {
        CheckItem();
        TryAction();
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            pressed_button = true;
            CheckItem();
            CanPickUp();
        }
        else if (get_key == true || mon.activeSelf == false)
        {
            MonsterActivation();
        }
    }

    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform != null && hitInfo.transform.CompareTag("Item"))
            {
                ItemInfoAppear();
            }
            else if (hitInfo.transform != null && hitInfo.transform.CompareTag("Exit"))
            {
                if (pressed_button)
                {
                    Checked_Key();
                }
            }
        }
        else
        {
            ItemInfoDisappear();
        }
    }

    private void Checked_Key()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (get_key && hitInfo.transform.CompareTag("Exit"))
            {
                SceneManager.LoadScene("SceneLoad");
            }
            else if (hitInfo.transform.CompareTag("Exit"))
            {
                actionText.gameObject.SetActive(true);
                actionText.text = "<color=red>" + " 카드 키가 없습니다" + "</color>";
                actionText.fontSize = 18;
                Invoke("ItemInfoDisappear", 5);
            }
        }
    }

    private void MonsterActivation()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.CompareTag("Activation") && get_key == true)
            {
                mon.SetActive(true);
            }
        }
    }

    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 " + "<color=red>" + "(E)" + "</color>";

        // KeycardBackgroundMusic 스크립트에서 배경 음악 시작
      
    }

    private void ItemInfoDisappear()
    {
        pickupActivated = false;
        pressed_button = false;
        actionText.gameObject.SetActive(false);
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null && hitInfo.transform.CompareTag("Item"))
            {
                string itemName = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName;
                Debug.Log(itemName + " 획득 했습니다.");
                get_key = true;

                // 소리 재생
                if (pickupSound != null)
                    pickupSound.Play();
                if (keycardBackgroundMusicScript != null)
                    keycardBackgroundMusicScript.StartBackgroundMusic();
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}