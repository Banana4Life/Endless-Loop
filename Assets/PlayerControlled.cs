using System;
using UnityEngine;

public class PlayerControlled : MonoBehaviour
{
    public float speed = 10f;
    public float gridSpeed = 2.5f;

    public float maxMoveCooldown = 1;

    public bool gridMove;

    private float _moveCooldown;

    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveCooldown > 0)
        {
            _moveCooldown -= Time.deltaTime;
        }

        var hMove = Input.GetAxisRaw("Horizontal");
        var vMove = Input.GetAxisRaw("Vertical");

        if (gridMove)
        {
            if (Math.Abs(hMove) + Math.Abs(vMove) > 0)
            {
                if (_moveCooldown <= 0)
                {
                    _moveCooldown = maxMoveCooldown;
                    transform.position += new Vector3(Math.Sign(hMove), 0, Math.Sign(vMove)) * gridSpeed;
                }
            }
        }
        else
        {
            if (Math.Abs(hMove) + Math.Abs(vMove) > 0)
            {
                _rigidbody.AddForce(new Vector3(hMove, 0, vMove).normalized * speed);    
            }
            else
            {
                _rigidbody.AddForce(new Vector3(hMove, 0, vMove).normalized * speed);
            }
            
        }
    }
}