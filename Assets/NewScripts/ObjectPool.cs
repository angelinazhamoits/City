using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    public static ObjectPool SharedInstance;

    [SerializeField] private List<GameObject> _poolObjects;
    [SerializeField] private GameObject _objectToPool;
    [SerializeField] private int _amountToPool;

    private void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        _poolObjects = new List<GameObject> ();
        GameObject poolTemp;
        for (int i = 0; i < _amountToPool; i++)
        {
            poolTemp = Instantiate(_objectToPool);
            poolTemp.SetActive(false);
            _poolObjects.Add(poolTemp);
        }
    }

    public GameObject GetPoolesObject()
    {
        for (int i = 0; i < _amountToPool; i++)
        {
            if (!_poolObjects[i].activeInHierarchy)
            {
                return _poolObjects[i];
            }
        }

        return null;
    }
    
}
