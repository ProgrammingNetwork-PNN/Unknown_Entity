using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Transform targetDoor; // ∏Ò«• πÆ

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TeleportPlayer(other.transform);
        }
    }

    private void TeleportPlayer(Transform playerTransform)
    {
        playerTransform.position = targetDoor.position;
    }
}