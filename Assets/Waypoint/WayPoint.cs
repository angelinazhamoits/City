using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{

   public WayPoint _previousWaypoint;
   public WayPoint _nextWaypoint;
    [Range(0f, 5f)] public float _width = 1f;

    public Vector3 GetPosition()
    {
        Vector3 minBounds = transform.position + transform.right * _width / 2f;
        Vector3 maxBounds = transform.position - transform.right * _width / 2f;

        return Vector3.Lerp(minBounds, maxBounds, Random.Range(0f, 1f));
    }
}
    
