using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Detection"))
        {
            Debug.Log("Epic fortnite victory royale");
        }
    }
}
