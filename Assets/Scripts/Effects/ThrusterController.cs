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
    public bool isThrusting;
}
