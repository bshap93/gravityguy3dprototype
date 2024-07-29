using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalShipController : MonoBehaviour
{
    public GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Thruster"))
        {
            gameManager.GetComponent<GameManager>().GameOver();
        }
    }
}
