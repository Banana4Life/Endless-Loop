using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    public bool open;
    private Animator _animator;
    private AudioSource _audio;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var isOpen = _animator.GetBool("IsOpen");
        if (open != isOpen)
        {
            _animator.SetBool("IsOpen", open);
            // _audio.Play();
        }
    }
}
