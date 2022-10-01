using System;
using UnityEngine;

public class PlayerControlled : MonoBehaviour
{
    public float moveSpeed = 10f;

    public bool onGround = true;
    public float groundDrag = 1;
    
    private float _moveCooldown;
    private float _boostCooldown;
    public float maxBoostCooldown = 2;
    public float boostSpeed = 2;

    private Rigidbody _rb;
    private float _hMove;
    private float _vMove;
    private bool _dashPress;

    public Transform orientation;
    public Transform playerModel;

    public float rotationSpeed = 50;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _dashPress = Input.GetKey(KeyCode.Space);
    }

    // Update is called once per frame
    void Update()
    {
        if (_moveCooldown > 0)
        {
            _moveCooldown -= Time.deltaTime;
        }
        if (_boostCooldown > 0)
        {
            _boostCooldown -= Time.deltaTime;
        }
        

        GetInputs();
        CapSpeed();
        UpdateDrag();
    }

    private void UpdateDrag()
    {
        _rb.drag = onGround ? groundDrag : 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        if (Math.Abs(_hMove) + Math.Abs(_vMove) > 0)
        {
            var inputDir = new Vector3(_hMove, 0, _vMove).normalized;
            _rb.AddForce(inputDir * moveSpeed, ForceMode.Force);
            if (_dashPress && _boostCooldown <= 0)
            {
                _boostCooldown = maxBoostCooldown;
                _rb.AddForce(inputDir * boostSpeed, ForceMode.Force);
            }
            playerModel.forward = Vector3.Slerp(playerModel.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }

    private void CapSpeed()
    {
        var flatVelocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
        if (flatVelocity.magnitude > moveSpeed)
        {
            var cappedSpeed = flatVelocity.normalized * moveSpeed;
            _rb.velocity = new Vector3(cappedSpeed.x, _rb.velocity.y, cappedSpeed.z);
        }
    }

    private void GetInputs()
    {
        _hMove = Input.GetAxisRaw("Horizontal");
        _vMove = Input.GetAxisRaw("Vertical");
    }
}