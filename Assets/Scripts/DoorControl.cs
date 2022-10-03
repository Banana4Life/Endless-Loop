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

    public AudioSource closeAudioSource;
    public AudioSource openAudioSource;
    public AudioSource useKeyCardAudioSource;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (randomOpen)
        {
            open = Random.value > 0.5;
            _animator.SetBool("IsOpen", open);
        }
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

    private void playAudio()
    {
        if (open)
        {
            openAudioSource.Play();
        }
        else
        {
            closeAudioSource.Play();
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
        playAudio();
        playActivated();
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
        playAudio();
    }

    public void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerCollider>();
        if (player && player.itemsInInventory.Contains(requiredKey))
        {
            if (!open)
            {
                open = true;
                _animator.SetBool("IsOpen", open);
                playAudio();
                playActivated();    
            }
            else
            {
                // TODO denied sound?
            }
        }
    }

    private void playActivated()
    {
        useKeyCardAudioSource.Play();
    }
}
