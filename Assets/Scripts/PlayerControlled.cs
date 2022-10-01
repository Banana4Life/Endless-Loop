using System;
using Unity.VisualScripting;
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
    public float moveSpeedAcc = 10f;
    public float moveSpeedCap = 10f;
    public float rotationSpeed = 50;
    private float _hMove;
    private float _vMove;

    [Header("Ground")] 
    public bool onGround = true;
    public float groundDrag = 5;
    public float airDrag = 2;
    public LayerMask whatIsGround;
    public float playerHeight;
 

    [Header("Dashing")]
    public float dashAcc = 30;
    public float dashAccUp = 5;
    public float dashDuration = 0.25f;
    private bool _dashing = false;
    private float _dashCdTimer;
    public float dashCd = 2;
    public float dashSpeedCap = 15f;
    private bool _dashPress;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        playerHeight = playerModel.GetComponent<CapsuleCollider>().height;
    }

    // Update is called once per frame
    void Update()
    {
        
        onGround = Physics.Raycast(playerModel.position, Vector3.down, 0.2f + playerHeight / 2, whatIsGround);
        
        if (_dashCdTimer > 0)
        {
            _dashCdTimer -= Time.deltaTime;
        }
        
        
        GetInputs();
        CapSpeed();
        UpdateDrag();
  

        cam.transform.position = camHolder.position;
    }

    private void UpdateDrag()
    {
        _rb.drag = onGround ? groundDrag : airDrag;
    }

    private void FixedUpdate()
    {
        if (onGround)
        {
            if (_dashPress && !_dashing)
            {
                Dash();
            }
            else
            {
                MovePlayer();
            }
        }
    }

    private void Dash()
    {
        if (_dashCdTimer > 0)
        {
            return;
        }

        _dashCdTimer = dashCd;
        _dashing = true;
        var rbMass = _rb.mass;
        var forceToApply = dashAcc * rbMass * playerModel.forward + dashAccUp * rbMass * playerModel.up;
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
            _rb.AddForce(moveSpeedAcc * _rb.mass * inputDir, ForceMode.Force);

            playerModel.forward = Vector3.Slerp(playerModel.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
    }

    private void CapSpeed()
    {
        var rbVelocity = _rb.velocity;
        var flatVelocity = new Vector3(rbVelocity.x, 0, rbVelocity.z);

        var speedCap = moveSpeedCap;
        if (_dashing)
        {
            speedCap = dashSpeedCap;
        }
        if (flatVelocity.magnitude > speedCap)
        {
            var cappedSpeed = flatVelocity.normalized * speedCap;
            cappedSpeed = Vector3.Lerp(flatVelocity, cappedSpeed, Time.deltaTime);
            _rb.velocity = new Vector3(cappedSpeed.x, rbVelocity.y, cappedSpeed.z);
        }
    }

    private void GetInputs()
    {
        _hMove = Input.GetAxisRaw("Horizontal");
        _vMove = Input.GetAxisRaw("Vertical");
        _dashPress = Input.GetKey(KeyCode.Space);
    }

}