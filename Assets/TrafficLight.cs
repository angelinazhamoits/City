using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TrafficLight : MonoBehaviour
{

    [SerializeField] private GameObject _greenLight;
    [SerializeField] private GameObject _redLight;
    public bool isGreen;
   
    private void Start()
    {
        StartChangeLight();
    }
    
    private void StartChangeLight()
    {
        StartCoroutine(LightChanger());
    }

    IEnumerator LightChanger () 
    {
        int counter = 10;
        while (counter > 0) {
            yield return new WaitForSeconds (1);
            counter--;
        }

        isGreen = false;
        _greenLight.SetActive(isGreen);
        _redLight.SetActive(!isGreen);
        Debug.Log($"isGreen: {isGreen}");
        StartChangeLight();
    }
}
