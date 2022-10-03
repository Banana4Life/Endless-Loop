using UnityEngine;

public class DoorControl : MonoBehaviour, Pressable
{
    public bool open;
    private Animator _animator;
    public PickupData requiredKey;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        // For Editor
        var isOpen = _animator.GetBool("IsOpen");
        if (open != isOpen)
        {
            _animator.SetBool("IsOpen", open);
        }
    }

    public void Press()
    {
        open = !open;
    }

    public void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerCollider>();
        if (player && player.itemsInInventory.Contains(requiredKey))
        {
            open = true;
            _animator.SetBool("IsOpen", open);
        }
    }
}
