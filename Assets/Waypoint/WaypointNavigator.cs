using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  WayointNavigator : MonoBehaviour
{
    private CharacterNavigationController _controller;
    internal WayPoint _currentWaypoint;

    private void Awake()
    {
        _controller.GetComponent<CharacterNavigationController>();
    }
}
