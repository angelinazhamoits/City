using System;
using System.Collections;
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
    [SerializeField] private float _playerSpeed;
    [SerializeField] private float _rotationSpeed = 2f;
    [SerializeField] private float _animationSmoothTime = 0.2f;
    
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
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MovedCharacter();
        RotateToDirection();
    }

    private void MovedCharacter()
    {
        _currentBlendAnimation = Vector2.SmoothDamp(_currentBlendAnimation, _moveInput, ref _animationVelocity,
            _animationSmoothTime);
        _move = new Vector3(_currentBlendAnimation.x, 0, _currentBlendAnimation.y);
        
        _move = _cameraTransform.right * _moveInput.x +_cameraTransform.forward * _moveInput.y;
        _move.y = 0;
        _characterController.Move(_move * Time.deltaTime * _playerSpeed);
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
