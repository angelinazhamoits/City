using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public GameObject Obj;

    private void Awake()
    {
        Obj.SetActive(false);
    }

    public void OnEnableAnimationEvent(string str)
    {
        Obj.SetActive(true);
    }
    
}
