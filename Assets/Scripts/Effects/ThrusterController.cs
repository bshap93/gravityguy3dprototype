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
}
