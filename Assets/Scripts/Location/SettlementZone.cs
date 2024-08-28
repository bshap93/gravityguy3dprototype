using System;
using System.Collections;
using System.Collections.Generic;
using SpaceGraphicsToolkit;
using UnityEngine;
using UnityEngine.Serialization;

public class SettlementZone : MonoBehaviour
{
    [SerializeField] SphereCollider settlementZoneShpere;

    [SerializeField] float radius = 50;

    Transform _settlementZoneCenter;
    void Start()
    {
        settlementZoneShpere = gameObject.GetComponent<SphereCollider>();
        settlementZoneShpere.radius = radius;
        _settlementZoneCenter = GetComponentInParent<Transform>();
    }


    void OnTriggerEnter(Collider other)
    {
        // try getting SgtGravityReceiver component
        var gravityReceiver = other.GetComponent<SgtGravityReceiver>();
        if (gravityReceiver != null)
        {
            gravityReceiver.enabled = false;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // try getting SgtGravityReceiver component
        var gravityReceiver = other.GetComponent<SgtGravityReceiver>();
        if (gravityReceiver != null)
        {
            gravityReceiver.enabled = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        // try getting SgtGravityReceiver component
        var gravityReceiver = other.GetComponent<SgtGravityReceiver>();
        if (gravityReceiver != null)
        {
            gravityReceiver.enabled = false;
        }
    }
}
