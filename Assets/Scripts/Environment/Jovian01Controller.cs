using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Jovian01Controller : MonoBehaviour
{
    private AudioSource _audioSource;
    [FormerlySerializedAs("EventManager")] public EventManager eventManager;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        _audioSource.Play();
        eventManager.StartFadeToBlack();
    }
}
