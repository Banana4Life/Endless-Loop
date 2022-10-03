using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DoorControl : MonoBehaviour, Pressable
{
    public bool open;
    private Animator _animator;
    public PickupData requiredKey;
    public float autoclose = 0;
    public float cd = 1;
    private float _cdTimer;
    public bool randomOpen;
    private void Start()
    {
        if (randomOpen)
        {
            open = Random.value > 0.5;
        }

        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_cdTimer > 0)
        {
            _cdTimer -= Time.deltaTime;
        }
        // For Editor
        var isOpen = _animator.GetBool("IsOpen");
        if (open != isOpen)
        {
            _animator.SetBool("IsOpen", open);
        }
    }

    public void Press()
    {
        if (_cdTimer > 0)
        {
            Debug.Log("Cannot Press during cd");
            return;
        }
        _cdTimer = cd;
        open = !open;
        Debug.Log("Next Door State: " + open + " " + name);
        if (open)
        {
            if (autoclose > 0)
            {
                Invoke(nameof(CloseDoor), autoclose);
            }
        }
    }

    public void CloseDoor()
    {
        open = false;
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
