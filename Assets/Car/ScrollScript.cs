using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollScript : MonoBehaviour
{

    [SerializeField] private WheelCollider _rfCollider;
    [SerializeField] private WheelCollider _lfCollider;
    [SerializeField] private WheelCollider _lbCollider;
    [SerializeField] private WheelCollider _rbCollider;
    
    [SerializeField] private Transform _rfTransform;
    [SerializeField] private Transform _lfTransform;
    [SerializeField] private Transform _lbTransform;
    [SerializeField] private Transform _rbTransform;

    [SerializeField] private float _accseleration = 500f;
    [SerializeField] private float _breakForce = 400f;
    [SerializeField] private float _maxTurnAngle = 15f;
    
     private float _currentAccseleration;
   private float _currentBreakForce;
   private float _currentTurnAngle;

   private void FixedUpdate()
   {
       _currentAccseleration = Input.GetAxis("Vertical") * _accseleration;
       if (Input.GetKey(KeyCode.Space))
       {
           _currentBreakForce = _breakForce;
       }
       else
       {
           _currentBreakForce = 0f;
       }

       _rfCollider.motorTorque = _currentAccseleration;
       _lfCollider.motorTorque = _currentAccseleration;

       _rfCollider.brakeTorque = _breakForce;
       _lfCollider.brakeTorque = _breakForce;
       _rbCollider.brakeTorque = _breakForce;
       _lbCollider.brakeTorque = _breakForce;

       _currentTurnAngle = Input.GetAxis("Horizontal") * _maxTurnAngle;
       _rfCollider.steerAngle = _currentTurnAngle;
       _lfCollider.steerAngle = _currentTurnAngle;
   }

   // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
