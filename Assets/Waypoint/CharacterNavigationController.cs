using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigationController : MonoBehaviour
{
   [SerializeField] private float _movementSpeed;
   [SerializeField] private float _stopDistance;
   [SerializeField] private float _rotationSpeed;
   private bool _isStay;

   private Vector3 _destination;
   private WayPoint _wayPoint;
   [SerializeField] private string _name;
   
   public bool _isReachedDestination;
   

   private void Update()
   {
      CheckTrafficLightValue();
      if (_isStay)
      {
         _movementSpeed = 0;
      }
      else
      {
         _movementSpeed = 5f;
      }
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

   public void SetDestination(ref WayPoint wayPoint, string pointName)
   {
      
      _wayPoint = wayPoint;
      _destination = wayPoint.GetPosition();
      _name = pointName;
      _isReachedDestination = false;
      Debug.Log($"addWaypoint:{_wayPoint.name}");
      
   }

   private void CheckTrafficLightValue()
   {
      /*if (_wayPoint.haveTrafficLight)
      {
         _isStay = !_wayPoint.trafficLight.isGreen;
         Debug.Log($"!_wayPoint.trafficLight.isGreen:{!_wayPoint.trafficLight.isGreen}");
      }*/
   }
}
