using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigationController : MonoBehaviour
{
   [SerializeField] private float _movementSpeed;
   [SerializeField] private float _stopDistance;
   [SerializeField] private float _rotationSpeed;

   private Vector3 _destination;
   [SerializeField] private string _name;
   
   public bool _isReachedDestination;

   private void Update()
   {
      if (transform.position != _destination)
      {
         Vector3 destinationDirection = _destination - transform.position;
         destinationDirection.y = 0;

         float destinationDistance = destinationDirection.magnitude;

         if (destinationDistance >= _stopDistance)
         {
            _isReachedDestination = false;
            Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed *Time.deltaTime);
            transform.Translate(Vector3.forward *_movementSpeed*Time.deltaTime);
         }
         else
         {
            _isReachedDestination = true;
         }
      }
   }

   public void SetDestination(Vector3 pointPosition, string pointName)
   {
      _destination = pointPosition;
      _name = pointName;
      _isReachedDestination = false;
   }
}
