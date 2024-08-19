using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [SerializeField] public GameObject shapeVisual;
    [SerializeField] public GameObject decalVisual;
    [SerializeField] public GameObject thruster;
    [SerializeField] public GameObject colliders;


    void Start()
    {
        shapeVisual = GameObject.Find("Shape Visual");
        decalVisual = GameObject.Find("Decal Visual");
        thruster = GameObject.Find("Thruster(Clone)");
        colliders = GameObject.Find("Colliders");
    }
}
