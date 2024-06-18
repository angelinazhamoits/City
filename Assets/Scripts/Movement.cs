using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private LayerMask _groundLevel;

    private bool _isGrounded;
    private Rigidbody _rb;

    private Vector3 _movementVector
    {
        get
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            return new Vector3(horizontal, 0, vertical);
        }
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        if (_groundLevel == gameObject.layer)
        {
            Debug.LogError($"Player SortingLayer must be different from Ground SortingLayer");
        }
    }

    private void FixedUpdate()
    {
        MovementLogic();
        JumpLogic();
    }

    private void MovementLogic()
    {
        _rb.AddForce(_movementVector * _speed, ForceMode.Impulse);

    }

    private void JumpLogic()
    {
        if (_isGrounded && Input.GetAxis("Jump") > 0)
        {
            if (_isGrounded)
            {
                 _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
           
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        IsGroundedUpdate(other, true);
    }

    private void OnCollisionExit(Collision other)
    {
        IsGroundedUpdate(other, false);
    }

    private void IsGroundedUpdate(Collision collision, bool value)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGrounded = value;
        }
    }
}
