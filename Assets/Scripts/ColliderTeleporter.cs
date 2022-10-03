using UnityEngine;

public class ColliderTeleporter : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayerCollider>();
        if (player)
        {
            player.SetLastTeleport(transform.position);
        }
    }
}
