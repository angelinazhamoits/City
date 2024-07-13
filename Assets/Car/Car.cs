using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Car : MonoBehaviour
{
   [SerializeField] private GameObject car;
   [SerializeField] private   GameObject player;
   [SerializeField] private GameObject textUI;
   [SerializeField] private CinemachineVirtualCamera carCamera;
    private bool isInCar = false;
    [SerializeField] private Animator carAnimator;
    private Rigidbody _rigidbody;
    void Start()
    {
        carAnimator = car.GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        textUI.SetActive(false);
    }
    void Update()
    {
        if (Vector3.Distance(player.transform.position, car.transform.position) < 5f)
        {
            textUI.SetActive(true);
        }
        else
        {
            textUI.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(player.transform.position, car.transform.position) < 5f)
        {
            if (!isInCar)
            {
                
                EnterCar();
            }
            else
            {
                ExitCar();
            }
        }
    }
    void EnterCar()
    {
       
        player.transform.position = car.transform.position;
        player.transform.rotation = car.transform.rotation; 
        carAnimator.SetBool("Sit", true);
        textUI.SetActive(false);
        carCamera.gameObject.SetActive(true);
        carCamera.Priority = 10;
        isInCar = true;
      
    }
    void ExitCar()
    {
        carCamera.gameObject.SetActive(false);
        carCamera.Priority = 0;
        player.GetComponent<CharacterController>().enabled = true;
        player.transform.position = car.transform.position + new Vector3(2f, 0f, 2f);
        player.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
        carAnimator.SetBool("Sit", false);
        isInCar = false;
    }
    
}

