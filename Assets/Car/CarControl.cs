using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore;

public class CarControl : MonoBehaviour
{
   [SerializeField] private float _motorTorque = 500f;
   [SerializeField] private float _breakTorgue = 300f;
   [SerializeField] private float _maxSpeed = 5f;
   [SerializeField] private float _steerengeRange = 15f;
   [SerializeField] private float _steerengeRangeAtMaxSpeed = 10f;
   [SerializeField] private float _centreOfGravitatyOffset = -1f;

   private WheelControl[] _wheel;
   private Rigidbody _rb;

   private void Start()
   {
      _rb = GetComponent<Rigidbody>();

      _rb.centerOfMass += Vector3.up * _centreOfGravitatyOffset;
      
     // _rb.centerOfMass = Vector3.zero;

      _wheel = GetComponentsInChildren<WheelControl>();
   }

   private void Update()
   {
      float vAxis = Input.GetAxis("Vertical");
      float hAxis = Input.GetAxis("Horizontal");

      float forwardSpeed = Vector3.Dot(transform.forward, _rb.velocity);
      float speedFactor = Mathf.InverseLerp(0, _maxSpeed, forwardSpeed);
      float currentMotorTorque = Mathf.Lerp(_motorTorque, 0, speedFactor);
      float currentSteerRange = Mathf.Lerp(_steerengeRange, _steerengeRangeAtMaxSpeed, speedFactor);

      bool isAccleration = Mathf.Sign(vAxis) == Mathf.Sign(forwardSpeed);
      foreach (var wheel in _wheel)
      {
         if (wheel._steerable)
         {
            wheel.WheelCollider.steerAngle = hAxis * currentSteerRange;
         }

         if (isAccleration)
         {
            if (wheel._motorized)
            {
               wheel.WheelCollider.motorTorque = vAxis * currentMotorTorque;
            }

            wheel.WheelCollider.brakeTorque = 0f;
         }
         else
         {
            wheel.WheelCollider.brakeTorque = Mathf.Abs(vAxis) * _breakTorgue;
            wheel.WheelCollider.motorTorque = 0f;
         }
      }

   }
}
