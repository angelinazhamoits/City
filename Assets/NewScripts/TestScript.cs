using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isSeat;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _isSeat = true;
            _animator.SetBool("isSeat", true);
        }
    }
}
