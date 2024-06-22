using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;
    private GameObject _focalPoint;
    public float accelerationFactor = 0.3f;
    public float rotationalFactor = 0.1f;
    public GameObject guidingLine;
    
    private float _horizontalInput;
    private float _verticalInput;

    // Start is called before the first frame update
    void Start()
    {
       _playerRb = GetComponent<Rigidbody>();
       _focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {
        ApplyThrust();
        ApplyRotationalThrust();
        ApplyBraking();
        LineUpShot();
        LaunchProjectile();        



    }
    
    // Poll for input and apply thrust to the player
    void ApplyThrust()
    {
        _verticalInput = Input.GetAxis("Vertical");
        
        
        // Apply relative force to the player 
        if (_verticalInput > 0)
        {
            _playerRb.AddRelativeForce(Vector3.forward * accelerationFactor, ForceMode.Impulse);
        }
        else if (_verticalInput < 0)
        {
            _playerRb.AddRelativeForce(Vector3.back * accelerationFactor, ForceMode.Impulse);
        }
        
    }
    
    // Poll for input and apply braking to the player
    void ApplyBraking()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            // Braking logic here
            if (_playerRb.velocity.magnitude > 0)
            {
                _playerRb.velocity -= _playerRb.velocity * 0.01f; 
                // _playerRb.totalTorque -= _playerRb.totalTorque * 0.01f;
            }
            
            if (_playerRb.angularVelocity.magnitude > 0)
            {
                _playerRb.angularVelocity -= _playerRb.angularVelocity * 0.01f; 
            } else if (_playerRb.angularVelocity.magnitude < 0)
            {
                _playerRb.angularVelocity += _playerRb.angularVelocity * 0.01f; 
            }


        }

    }
    
    // Poll for input and apply rotational thrust to the player
    void ApplyRotationalThrust()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        
        _playerRb.AddTorque( _focalPoint.transform.up * (_horizontalInput * rotationalFactor), ForceMode.Impulse);
    }

    void LineUpShot()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            guidingLine.gameObject.SetActive(true);
            
        }
        else
        {
            guidingLine.gameObject.SetActive(false);
        }
        
    }
    
    void LaunchProjectile()
    {
        if (Input.GetMouseButtonDown( 0))
        {
            // Launch projectile logic here
            print("Launching projectile!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {

    }



    private void OnCollisionEnter(Collision other)
    {

    }
}
