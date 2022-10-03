using UnityEngine;

public class ColliderPickup : MonoBehaviour
{
    public PickupData data;
    public AudioSource pickupAudio;

    void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayerCollider>();
        if (player)
        {
            player.Pickup(data);
            Destroy(transform.parent.gameObject);
            if (pickupAudio)
            {
                pickupAudio.Play();
            }
        }
    }

    public void Init(PickupData data)
    {
        pickupAudio = data.model.GetComponent<AudioSource>();
        this.data = data;
    }
}