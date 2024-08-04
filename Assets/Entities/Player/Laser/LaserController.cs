using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

public class LaserController : MonoBehaviour
{
    public float laserStrength;
    public float laserRange;
    public GameObject laserTurretBarrel;
    public GameObject laserTurretGun;

    [FormerlySerializedAs("_laserAudioSource")]
    public AudioSource laserAudioSource;
    private LineRenderer _lineRenderer;

    void Start()
    {
        laserAudioSource = GetComponent<AudioSource>();
        if (laserAudioSource == null)
        {
            Debug.LogWarning("AudioSource component is missing. Adding one.");
            laserAudioSource = gameObject.AddComponent<AudioSource>();
            // You might want to set up some default audio clip and settings here
        }

        _lineRenderer = GetComponent<LineRenderer>();
        if (_lineRenderer == null)
        {
            Debug.LogWarning("LineRenderer component is missing. Adding one.");
            _lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        SetupLineRenderer();
    }

    void SetupLineRenderer()
    {
        if (_lineRenderer != null)
        {
            _lineRenderer.startColor = Color.red;
            _lineRenderer.endColor = Color.yellow;
            _lineRenderer.startWidth = 0.1f;
            _lineRenderer.endWidth = 0.1f;
            _lineRenderer.positionCount = 2;
        }
        else
        {
            Debug.LogError("LineRenderer component is still missing after attempting to add it.");
        }
    }

    public void FireLaser(Vector3 targetPosition)
    {
        if (_lineRenderer == null)
        {
            Debug.LogError("LineRenderer is missing. Cannot fire laser.");
            return;
        }

        Vector3 laserTurretHardPoint = laserTurretBarrel.transform.position;

        laserTurretGun.transform.LookAt(targetPosition);

        _lineRenderer.SetPosition(0, laserTurretHardPoint);
        _lineRenderer.SetPosition(1, targetPosition);

        if (laserAudioSource != null && laserAudioSource.clip != null)
        {
            laserAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or audio clip is missing. Cannot play laser sound.");
        }

        StartCoroutine(DeactivateLaser(0.2f));
    }

    IEnumerator DeactivateLaser(float delay)
    {
        yield return new WaitForSeconds(delay);
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.zero);
    }

    public void ApplyDamage(BasicAsteroidController asteroidController)
    {
        asteroidController.ReduceHitPoints(laserStrength);
    }
}
