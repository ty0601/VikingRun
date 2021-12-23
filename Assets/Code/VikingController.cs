using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;

public class VikingController : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerInput _playerInput;
    private Animator _animator;

    //player movement variables
    private Vector2 _movementInput;
    private Vector3 _currentMovement;
    public bool _isStart;

    public bool isAlive;
    public bool _isDrop = false;

    //jump variable
    private bool _isJumpPressed = false;
    private float _initialJumpVelocity = 2.5f;
    private float _maxJumpHeight = 0.5f;
    private bool _isJumping = false;

    //player rotate variables
    public float _targetAngle;
    public float _entryAngle;
    private bool _canRotate;
    private Vector2 _rotateInput;

    //gravity variable
    private float _gravity = -9f;
    private float _groundedGravity = -0.05f;

    //speed variable
    [SerializeField] float speed = 20f;
    [SerializeField] float horizontalSpeed = 15f;

    private void Awake()
    {
        isAlive = true;
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _playerInput = new PlayerInput();

        _playerInput.CharactorController.Move.started += OnMovementInput;
        _playerInput.CharactorController.Move.canceled += OnMovementInput;
        _playerInput.CharactorController.Move.performed += OnMovementInput;
        _playerInput.CharactorController.Jump.started += OnJump;
        _playerInput.CharactorController.Jump.canceled += OnJump;
        _playerInput.CharactorController.Rotate.started += OnRotate;
        _playerInput.CharactorController.Rotate.performed += OnRotate;
        _playerInput.CharactorController.Rotate.canceled += OnRotate;
    }

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnMovementInput(InputAction.CallbackContext context)
    {
        if (!isAlive) return;
        _movementInput = context.ReadValue<Vector2>();
        _currentMovement.x = _movementInput.x;
        _currentMovement.z = _movementInput.y;
    }

    private void OnRotate(InputAction.CallbackContext context)
    {
        if (!isAlive) return;

        if (!_canRotate)
        {
            if (_entryAngle == _targetAngle)
            {
                _rotateInput = context.ReadValue<Vector2>();
                Rotate();
            }
        }
        else
        {
            _rotateInput = context.ReadValue<Vector2>();
            _canRotate = false;

            Rotate();
        }
    }

    public bool CanRotate
    {
        get => _canRotate;
        set => _canRotate = value;
    }

    private void Rotate()
    {
        if (_rotateInput.x > 0)
        {
            _targetAngle = transform.eulerAngles.y + 90;
        }
        else if (_rotateInput.x < 0)
        {
            _targetAngle = transform.eulerAngles.y - 90;
        }
    }

    public void InitialRotation()
    {
        _targetAngle = 0f;
        _entryAngle = 0.1f;
    }

    private void HandleRotate()
    {
        if (CheckRotation())
        {
            _characterController.transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0, _targetAngle, 0), 15f * Time.deltaTime);
        }
    }

    private bool CheckRotation()
    {
        if (Math.Floor(transform.eulerAngles.y) == Math.Floor(_targetAngle < 0 ? 360 + _targetAngle : _targetAngle) ||
            Math.Ceiling(transform.eulerAngles.y) ==
            Math.Ceiling(_targetAngle < 0 ? 360 + _targetAngle : _targetAngle) ||
            Math.Ceiling(transform.eulerAngles.y) == Math.Floor(_targetAngle < 0 ? 360 + _targetAngle : _targetAngle) ||
            Math.Floor(transform.eulerAngles.y) == Math.Ceiling(_targetAngle < 0 ? 360 + _targetAngle : _targetAngle))
        {
            return false;
        }

        return true;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (!isAlive) return;
        _isJumpPressed = context.ReadValueAsButton();
    }


    private void HandleJump()
    {
        if (!_isJumping && _characterController.isGrounded && _isJumpPressed)
        {
            _isJumping = true;
            _currentMovement.y = _initialJumpVelocity;
        }
        else if (_isJumping && _characterController.isGrounded && !_isJumpPressed)
        {
            _isJumping = false;
        }
    }

    private void HandleGravity()
    {
        if (_characterController.isGrounded)
        {
            _currentMovement.y += _groundedGravity * Time.deltaTime;
        }
        else
        {
            _currentMovement.y += _gravity * Time.deltaTime;
        }
    }

    public void Die()
    {
        _animator.SetBool("isRun", false);
        if (!_isDrop) _animator.SetBool("isKnock", true);
        speed = 0f;
        horizontalSpeed = 0f;

        isAlive = false;
    }


    private void OnEnable()
    {
        _playerInput.CharactorController.Enable();
    }

    private void OnDisable()
    {
        _playerInput.CharactorController.Disable();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_isStart)
        {
            if (_currentMovement.z == 0)
            {
                _animator.SetBool("isRun", false);
            }
            else
            {
                _animator.SetBool("isRun", true);
                if (!_animator.GetBool("isRun"))
                {
                    Debug.Log("false");
                }
                _characterController.Move(speed * Time.deltaTime * transform.forward);
            }

            _canRotate = true;
        }
        else
        {
            _animator.SetBool("isRun", true);
            _characterController.Move(speed * Time.deltaTime * transform.forward);
        }

        if (transform.position.y <= -5)
        {
            _isDrop = true;
            Die();
        }

        HandleRotate();
        HandleGravity();


        _characterController.Move(speed * Time.deltaTime * new Vector2(0, _currentMovement.y));
        if(!_characterController.isGrounded && !_isDrop) _animator.SetBool("isjump",true);
        else _animator.SetBool("isjump",false);
        
        if (_currentMovement.x > 0)
        {
            if(!_animator.GetBool("isRun")) _animator.SetBool("isRun", true);
            _characterController.Move(horizontalSpeed * Time.deltaTime * transform.right);
        }
        else if (_currentMovement.x < 0)
        {
            if(!_animator.GetBool("isRun")) _animator.SetBool("isRun", true);
            _characterController.Move(horizontalSpeed * Time.deltaTime * transform.right * (-1));
        }
        else if (_currentMovement.x == 0 && _currentMovement.z == 0 && !_isStart)
        {
            _animator.SetBool("isRun", false);
        }


        HandleJump();
    }
}