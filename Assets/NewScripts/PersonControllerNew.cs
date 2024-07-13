using System;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PersonControllerNew : MonoBehaviour
{
    #region Bullet

      [SerializeField] private Transform _bulletParent;
       
        [SerializeField] private Transform _gunTransform;
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private float _bulletHitMiss = 25f;
        

    #endregion

    private CharacterController _characterController;
    
    
    private Vector3 _move;
    private Vector3 _moveInput;
    private Vector2 _currentBlendAnimation;
    private Vector2 _animationVelocity;
    [SerializeField] private Animator _animator; 
    [SerializeField] private float _playerSpeed;
    private bool _isGround;
    private Vector3 _playerVelocity;
    [SerializeField] private float _jumpHeight;
    private float _gravityValue = -9.81f;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _animationSmoothTime = 0.2f;
    [SerializeField] private float _animationPlayTransition = 0.15f;
    
    private int _moveXAnimationParametrId;
    private int _moveYAnimationParametrId;
    private int _jumpAnimation;
    
public Vector2 MoveIput
{
    set 
    { _moveInput.x = value.x; 
        _moveInput.y = value.y;
    }
}
    public bool isJump;
    
    private void Awake()
    {
        _cameraTransform = Camera.main.transform;
        _animator = GetComponent<Animator>();
        _moveXAnimationParametrId = Animator.StringToHash("MovementX");
        _moveYAnimationParametrId = Animator.StringToHash("MovementY");
        _jumpAnimation = Animator.StringToHash("Jump");

    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GroundCheck();
        MovedCharacter();
        JumpCharacter();
        RotateToDirection();
    }
    

    private void GroundCheck()
    {
        _isGround = _characterController.isGrounded;
        if (_isGround&& _playerVelocity.y<0)
        {
            _playerVelocity.y = 0;
        }
    }

    private void JumpCharacter()
    {
        if (_isGround&& isJump)
        {
            _playerVelocity.y = Mathf.Sqrt(_jumpHeight * -0.3f * _gravityValue);
            _animator.CrossFade(_jumpAnimation, _animationPlayTransition);
            isJump = false;
        }
        else
        {
            _playerVelocity.y += _gravityValue * Time.deltaTime;
            _characterController.Move(_playerVelocity * Time.deltaTime);
        }
    }

    private void MovedCharacter()
    {
        _currentBlendAnimation = Vector2.SmoothDamp(_currentBlendAnimation, _moveInput, ref _animationVelocity,
            _animationSmoothTime);
        _move = new Vector3(_currentBlendAnimation.x, 0, _currentBlendAnimation.y);
        
        _move = _cameraTransform.right * _moveInput.x +_cameraTransform.forward * _moveInput.y;
        _move.y = 0;
        _characterController.Move(_move * Time.deltaTime * _playerSpeed);
        
        _animator.SetFloat(_moveYAnimationParametrId, _currentBlendAnimation.y);
        _animator.SetFloat(_moveXAnimationParametrId, _currentBlendAnimation.x);
    }

    private void RotateToDirection()
    {
        if (_moveInput !=Vector3.zero)
        {
            Quaternion rotation = Quaternion.Euler(0f,_cameraTransform.eulerAngles.y, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _rotationSpeed);
        }
    }

    
    
    public void ShootGun()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPoolesObject();
        if (bullet != null)
        {
            bullet.transform.parent = _bulletParent;
            bullet.transform.position = _gunTransform.position;
            bullet.transform.rotation = _gunTransform.rotation;
            bullet.SetActive(true);
        }

        BulletController bulletController = bullet.GetComponent<BulletController>();
        RaycastHit hit;
        if (Physics.Raycast(_cameraTransform.position,_cameraTransform.forward, out hit, Mathf.Infinity ))
        {
            bulletController.Target = hit.point;
            bulletController.Hit = true;
        }
        else
        {
            bulletController.Target = _cameraTransform.position + _cameraTransform.forward * _bulletHitMiss;
            bulletController.Hit = false;
        }
    }
    
}
