using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnPedestrian : MonoBehaviour
{
    [SerializeField] private GameObject _pedestrianPrefab;
    [SerializeField] private int _pedestrianCount;

    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;
        while (count < _pedestrianCount)
        {
            GameObject obj = Instantiate(_pedestrianPrefab);
            Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
            obj.GetComponent<WaypointNavigator>()._currentWaypoint = child.GetComponent<WayPoint>();
            obj.transform.position = child.position;

            yield return new WaitForEndOfFrame();
            count++;
        }
    }
}
