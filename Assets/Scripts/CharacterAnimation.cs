using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimation : MonoBehaviour
{
  private Animator _animator;
  private float _isMoving;
    private void Start()
   {
      _animator = GetComponent<Animator>();
      
   }

   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
      {
         _animator.SetBool("isMove", true);
      }
      else
      {
         _animator.SetBool("isMove", false);
      }
   }
}
