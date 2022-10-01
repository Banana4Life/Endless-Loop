using System;
using UnityEngine;

public class PlayerControlled : MonoBehaviour
{
    public float speed = 5;

    public float maxMoveCooldown = 1;

    private float _moveCooldown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveCooldown > 0)
        {
            _moveCooldown -= Time.deltaTime;
        }

        var hMove = Input.GetAxis("Horizontal");
        var vMove = Input.GetAxis("Vertical");
        if (Math.Abs(hMove) + Math.Abs(vMove) > 0)
        {
            if (_moveCooldown <= 0)
            {
                
                _moveCooldown = maxMoveCooldown;
                transform.position += new Vector3(Math.Sign(hMove), 0, Math.Sign(vMove)) * speed;
            }
        }
    }
}
