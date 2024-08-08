using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class LaserController : MonoBehaviour
{
    public float laserStrength;
    public float laserRange;
    public GameObject laserTurretBarrel;
    public GameObject laserTurretGun;
    [FormerlySerializedAs("FX_Laser_shot")]
    public GameObject fxLaserShot;

    [FormerlySerializedAs("_laserAudioSource")]
    public AudioSource laserAudioSource;

    void Start()
    {
        laserAudioSource = GetComponent<AudioSource>();
        if (laserAudioSource == null)
        {
            Debug.LogWarning("AudioSource component is missing. Adding one.");
            laserAudioSource = gameObject.AddComponent<AudioSource>();
            // You might want to set up some default audio clip and settings here
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            Debug.Log("Firing");
        }
    }
}
