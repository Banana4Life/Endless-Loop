using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public Vector3 lastTeleportPosition;
    public void SetLastTeleport(Vector3 position)
    {
        lastTeleportPosition = position;
    }

    public void Pickup(PickupData data)
    {
        Debug.Log("Picked up " + data.itemName);
    }
}
