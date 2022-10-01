using System;
using UnityEngine;

public class PlayerControlled : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerModel;
    public Camera cam;
    public Transform camHolder;
    private Rigidbody _rb;

    [Header("Movement")]
    public float moveSpeed = 10f;
    public bool onGround = true;
    public float groundDrag = 1;
    public float rotationSpeed = 50;
    private float _hMove;
    private float _vMove;

    [Header("Dashing")]
    public float dashForce = 20;
    public float dashForceUp = 2;
    public float dashDuration = 0.25f;
    private bool _dashing = false;
    private float _dashCdTimer;
    public float dashCd = 2;
    public float dashSpeed = 15f;
    private bool _dashPress;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_dashCdTimer > 0)
        {
            _dashCdTimer -= Time.deltaTime;
        }
        
        
        GetInputs();
        CapSpeed();
        UpdateDrag();
        if (_dashPress && !_dashing)
        {
            Dash();
        }

        cam.transform.position = camHolder.position;
    }

    private void UpdateDrag()
    {
        _rb.drag = onGround ? groundDrag : 0;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Dash()
    {
        if (_dashCdTimer > 0)
        {
            return;
        }

        _dashCdTimer = dashCd;
        _dashing = true;
        var forceToApply = playerModel.forward * dashForce + playerModel.up * dashForceUp;
        _rb.AddForce(forceToApply, ForceMode.Impulse);
        Invoke(nameof(ResetDash), dashDuration);
    }

    private void ResetDash()
    {
        _dashing = false;
    }
    
    private void MovePlayer()
    {
        if (Math.Abs(_hMove) + Math.Abs(_vMove) > 0)
        {
            var camDir = cam.transform.forward;
            orientation.forward = new Vector3(camDir.x, 0, camDir.z);
            
            var inputDir = (orientation.forward * _vMove + orientation.right * _hMove).normalized;
            _rb.AddForce(inputDir * moveSpeed, ForceMode.Force);

            playerModel.forward = Vector3.Slerp(playerModel.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }

    private void CapSpeed()
    {
        var flatVelocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);

        var speedCap = moveSpeed;
        if (_dashing)
        {
            speedCap = dashSpeed;
        }
        if (flatVelocity.magnitude > speedCap)
        {
            var cappedSpeed = flatVelocity.normalized * speedCap;
            cappedSpeed = Vector3.Lerp(flatVelocity, cappedSpeed, Time.deltaTime);
            _rb.velocity = new Vector3(cappedSpeed.x, _rb.velocity.y, cappedSpeed.z);
        }
    }

    private void GetInputs()
    {
        _hMove = Input.GetAxisRaw("Horizontal");
        _vMove = Input.GetAxisRaw("Vertical");
        _dashPress = Input.GetKey(KeyCode.Space);
    }

}