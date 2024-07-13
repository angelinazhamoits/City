using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _mouseSensevitity = 100f;
    [SerializeField] private float _gravity = -9.81f;

    private CharacterController _characterController;
    private Vector3 _velocity;
    private float _rotationY = 0;
    private float _rotationX = 0;

    [SerializeField] private Transform _cameraTransform;


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Move();
        Rotate();
        ApplyGravity();
    }

    public void Move()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        _characterController.Move(move * _speed * Time.deltaTime);
        
    }

private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensevitity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensevitity * Time.deltaTime;

        _rotationY += mouseX;
        transform.localRotation = Quaternion.Euler(0f, _rotationY, 0f);

        _rotationX -= mouseY;
        _rotationX = Mathf.Clamp(_rotationX, -60f, 60f);
        _cameraTransform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f);
    }

    private void ApplyGravity()
    {
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
    }
    
}
