using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class  WaypointNavigator : MonoBehaviour
{
   [SerializeField] private CharacterNavigationController _controller;
   public WayPoint _currentWaypoint;

   private int _direction;

    private void Awake()
    {
        _direction = Mathf.RoundToInt(Random.Range(0f, 1f));
        _controller.GetComponent<CharacterNavigationController>();
    }

    private void Start()
    {
        _controller.SetDestination(_currentWaypoint.GetPosition(), _currentWaypoint.name);
        
    }

    private void Update()
    {
        if (_controller._isReachedDestination)
        {
            bool shoulBranch = false;
            if (_currentWaypoint.Branches != null && _currentWaypoint.Branches.Count > 0)
            {
                shoulBranch = Random.Range(0f,1f) <= _currentWaypoint._branchRatio ? true : false;
            }

            if (shoulBranch)
            {
                _currentWaypoint = _currentWaypoint.Branches[Random.Range(0, _currentWaypoint.Branches.Count - 1)];
            }
            else
            {
                if (_direction == 0)
                {
                    if (_currentWaypoint._nextWaypoint != null)
                    {
                        _currentWaypoint = _currentWaypoint._nextWaypoint;
                    }
                    else
                    {
                        _currentWaypoint = _currentWaypoint._previousWaypoint;
                        _direction = 1;
                    }
                }
                else if (_direction == 1)
                {
                    if (_currentWaypoint._previousWaypoint != null)
                    {
                        _currentWaypoint = _currentWaypoint._previousWaypoint;
                    }
                    else
                    {
                        _currentWaypoint = _currentWaypoint._nextWaypoint;
                        _direction = 0;
                    }
                    
                }

                
            }

        }
        _controller.SetDestination(_currentWaypoint.GetPosition(), _currentWaypoint.name);
   
    }
}
