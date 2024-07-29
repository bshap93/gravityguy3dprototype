using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum ThrusterRole
{
    Projectile,
    RearThruster,
    AttitudeJet
}

public class ThrusterController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    public GameObject thrusterParticleSystem;
    private bool _isAttachedToPlayer;
    
    private float _verticalInput;

    public ThrusterController(bool isAttachedToPlayer)
    {
        _isAttachedToPlayer = isAttachedToPlayer;
    }

    void Start()
    {
        thrusterParticleSystem.SetActive(false);
        _isAttachedToPlayer = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = player.transform.rotation;
        
    }
    


    public void LaunchProjectile()
    {
            _isAttachedToPlayer = false;

    }

    public void OnCollisionEnter(Collision other)
    {

    }

    public void RecallProjectile()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            player = GameObject.Find("Player");
            // move towards player
            transform.position = player.transform.position + new Vector3(0, 0, 3);
        }
    }
}
