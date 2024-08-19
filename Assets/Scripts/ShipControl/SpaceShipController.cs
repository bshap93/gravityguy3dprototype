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
    [SerializeField] public GameObject attitudeThrusterPrefab;


    void Start()
    {
        shapeVisual = GameObject.Find("Shape Visual");
        decalVisual = GameObject.Find("Decal Visual");
        thruster = GameObject.Find("Thruster(Clone)");
        colliders = GameObject.Find("Colliders");
        var attitudeThruster = Instantiate(attitudeThrusterPrefab, transform, true);
        attitudeThruster.transform.localPosition = new Vector3(0, 0, 0);
        attitudeThruster.transform.localRotation = new Quaternion(0, 0, 0, 1);
        attitudeThruster.transform.localScale = new Vector3(1, 1, 1);
    }
}
