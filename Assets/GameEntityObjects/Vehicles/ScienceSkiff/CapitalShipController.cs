using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapitalShipController : MonoBehaviour
{
    public GameObject gameManager;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _audioSource.Play();
        }
    }
}
