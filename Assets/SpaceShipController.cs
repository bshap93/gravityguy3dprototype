using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    [SerializeField] GameObject shapeVisual;
    [SerializeField] GameObject decalVisual;
    [SerializeField] GameObject thruster;
    [SerializeField] GameObject colliders;


    // Start is called before the first frame update
    void Start()
    {
        shapeVisual = GameObject.Find("Shape Visual");
        decalVisual = GameObject.Find("Decal Visual");
        thruster = GameObject.Find("Thruster(Clone)");
        colliders = GameObject.Find("Colliders");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
