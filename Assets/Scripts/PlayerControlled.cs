using System;
using UnityEngine;

public class PlayerControlled : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerModel;
    private Camera _cam;
    public Transform camHolder;
    private Rigidbody _rb;
    public ParticleSystem walkingParticleSystem;
    public ParticleSystem dashingParticleSystem;

    [Header("Movement")]
    public float moveSpeedAcc = 10f;
    public float moveSpeedCap = 10f;
    public float rotationSpeed = 50;
    private float _hMove;
    private float _vMove;
    public float animationSpeed = 0.2f;
    private Animator _animator;
    public bool movementEnabled = true;

    [Header("Ground")] 
    public bool onGround = true;
    public float groundDrag = 5;
    public float airDrag = 2;
    public LayerMask whatIsGround;
    private CapsuleCollider _playerCapsule;

    [Header("Dashing")]
    public float dashAcc = 30;
    public float dashAccUp = 5;
    public float dashDuration = 0.25f;
    private bool _dashing = false;
    private float _dashCdTimer;
    public float dashCd = 2;
    public float dashSpeedCap = 15f;
    private bool _dashPress;
    private static readonly int IsWalkingAnimationProperty = Animator.StringToHash("Is Walking");
    private static readonly int WalkSpeedAnimationProperty = Animator.StringToHash("Walk Speed");

    [Header("Cam Follow")] 
    public float camSpeed = 5;
    
    void Start()
    {
        _cam = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _playerCapsule = playerModel.GetComponent<CapsuleCollider>();
        _animator = playerModel.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = Physics.Raycast(playerModel.position + _playerCapsule.center, Vector3.down, 0.2f + _playerCapsule.height / 2f, whatIsGround);
        
        if (_dashCdTimer > 0)
        {
            _dashCdTimer -= Time.deltaTime;
        }
        
        GetInputs();
        CapSpeed();
        UpdateDrag();
        var camTransform = _cam.transform;
        camTransform.position = Vector3.Lerp(camTransform.position, new Vector3(camHolder.position.x, camTransform.position.y, camHolder.position.z), Time.deltaTime * camSpeed);
        
        
    }

    private void UpdateDrag()
    {
        _rb.drag = onGround ? groundDrag : airDrag;
    }

    private void FixedUpdate()
    {
        if (movementEnabled && onGround)
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

        var speed = new Vector3(_rb.velocity.x, 0, _rb.velocity.z).magnitude * animationSpeed;
        var isWalking = speed > 0.1;
        _animator.SetBool(IsWalkingAnimationProperty, isWalking);
        _animator.SetFloat(WalkSpeedAnimationProperty, Mathf.Max(1, speed));
        if (movementEnabled && isWalking)
        {
            if (walkingParticleSystem.isStopped)
            {
                walkingParticleSystem.Play();
            }
        }
        else
        {
            walkingParticleSystem.Stop();
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
        dashingParticleSystem.Play();
    }

    private void ResetDash()
    {
        _dashing = false;
    }
    
    private void MovePlayer()
    {
        if (Math.Abs(_hMove) + Math.Abs(_vMove) > 0)
        {
            var camDir = _cam.transform.forward;
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

    public void EnableMovement()
    {
        movementEnabled = true;
    }

    public void DisableMovement()
    {
        movementEnabled = false;
    }
}