using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject _bulletDecals;
    [SerializeField] private float _speed = 20f;
    [SerializeField] private float _bulletDestroyTime = 5f;
    
    public Vector3 Target { get; set; }
    public bool Hit { get; set; }

    private void OnEnable()
    {
        StartCoroutine(DeactivateBooletAfterDelay(gameObject));
        
    }

    private  IEnumerator DeactivateBooletAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(_bulletDestroyTime);
        obj.SetActive(false);
    }

  
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, Target, _speed * Time.deltaTime);
        if (!Hit && Vector3.Distance(transform.position, Target) < 0.1f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        ContactPoint contact = other.GetContact(0);
        Instantiate(_bulletDecals, contact.point + contact.normal * 0.001f, Quaternion.LookRotation(contact.normal));
        gameObject.SetActive(false);
    }
}
