using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0, 360)] public float viewAngle;
    [SerializeField] private LayerMask _targetMask;
    [SerializeField] private LayerMask _obstacleMask;
    public List<Transform> VisibleTarget;

    [SerializeField] private float _delayTime = 0.2f;
    void Start()
    {
        StartCoroutine("FindTarget", _delayTime);
    }

    IEnumerator FindTarget(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }

    public void FindVisibleTarget()
    {
        List<Transform> visibleTarget = new List<Transform>();
        Collider[] targetInRadius = Physics.OverlapSphere(transform.position, viewRadius, _targetMask);
        for (int i = 0; i < targetInRadius.Length; i++)
        {
            Transform target = targetInRadius[i].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2) ;
           // Ray ray = new Ray(transform.position, directionToTarget);
           
                if (Physics.Raycast(transform.position, directionToTarget, _targetMask))
                {
                    VisibleTarget.Add(target);
                }
        }
       
    }
    

    public Vector3 DirectionFromAngle(float angleDegrees, bool isAngleGlobal)
    {
        if (!isAngleGlobal)
        {
              angleDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleDegrees * Mathf.Deg2Rad));
    }
}
