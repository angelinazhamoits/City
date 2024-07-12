using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TrafficLight : MonoBehaviour
{

    [SerializeField] private GameObject _greenLight;
    [SerializeField] private GameObject _redLight;
    public bool _isGreen;
   
    private void Start()
    {
        StartChangeLight();
    }
    
    private void StartChangeLight()
    {
        StartCoroutine(LightChanger());
    }

    IEnumerator LightChanger () {
        int counter = 10;
        while (counter > 0) {
            yield return new WaitForSeconds (1);
            counter--;
        }

        _isGreen = !_isGreen;
        _greenLight.SetActive(_isGreen);
        _redLight.SetActive(!_isGreen);
        StartChangeLight();
    }
}
