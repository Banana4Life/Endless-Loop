using UnityEngine;

public class TriggerListener : MonoBehaviour
{
    private PickupData _data;

    void OnTriggerEnter(Collider other)
    {
        var pickup = other.gameObject.GetComponent<PlayerCollider>();
        if (pickup)
        {
            Debug.Log("Picked up " + _data.itemName);
            Destroy(transform.parent.gameObject);
        }
    }

    public void Init(PickupData data)
    {
        _data = data;
    }
}