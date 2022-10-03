using UnityEngine;

public class ColliderPickup : MonoBehaviour
{
    private PickupData _data;

    void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayerCollider>();
        if (player)
        {
            player.Pickup(_data);
            Destroy(transform.parent.gameObject);
        }
    }

    public void Init(PickupData data)
    {
        _data = data;
    }
}