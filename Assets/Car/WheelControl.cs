using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelControl : MonoBehaviour
{
    public Transform WheelModel;

    public WheelCollider WheelCollider;

    public bool _steerable;
    public bool _motorized;

    private Vector3 _position;
    private Quaternion _rotation;
    
    void Start()
    {
        WheelCollider = GetComponent<WheelCollider>();
        
        
    }
    
    void Update()
    {
        WheelCollider.GetWorldPose(out _position, out _rotation);
        WheelModel.transform.position = transform.position;
        WheelModel.transform.rotation = transform.rotation;
        
    }
}
