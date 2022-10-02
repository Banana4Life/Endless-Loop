using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        var pickup = other.gameObject.GetComponent<PlayerCollider>();
        if (pickup)
        {
            Debug.Log("Teleporter Touched");
        }
    }
}
