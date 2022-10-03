using UnityEngine;

public class ColliderTeleporter : MonoBehaviour
{
    public AudioSource audioSource;
    void OnTriggerEnter(Collider other)
    {
        var player = other.gameObject.GetComponent<PlayerCollider>();
        if (player)
        {
            if (player.lastTeleportPosition != transform.position)
            {
                audioSource.Play();
            }
            player.SetLastTeleport(transform.position);
        }
    }
}
