using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    public Vector3 lastTeleportPosition;
    public Animator animator;
    private static readonly int HoldingWeaponProperty = Animator.StringToHash("Holding Weapon");
    public List<PickupData> itemsInInventory = new();
    public PickupData dashItem;
    public GameObject pickupAudioSources;

    public bool canDash()
    {
        return itemsInInventory.Contains(dashItem);
    }
    
    public void SetLastTeleport(Vector3 position)
    {
        lastTeleportPosition = position;
        Debug.Log("Teleporter Touched at " + position);
    }

    public void Pickup(PickupData data)
    {
        switch (data.itemName)
        {
            case "weapon":
                animator.SetBool(HoldingWeaponProperty, true);
                break;
        }
        itemsInInventory.Add(data);
        var found = pickupAudioSources.transform.Find(data.itemName);
        if (found)
        {
            found.GetComponent<AudioSource>().Play();
        }
        Debug.Log("Picked up " + data.itemName);
    }
}
