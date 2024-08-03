using System.Collections;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class BasicAsteroidController : MonoBehaviour
{
    public Vector3 initialRotationForceVector;
    public float RotationIntensity = 100;
    public GameObject dustCloudPrefab;
    public AudioClip dustCloudBurstSound;
    public AudioClip hitSound;
    public float maxHitPoints = 100f;
    public SphereCollider sphereColliderObject;
    public float dustCloudLifetime = 5f;
    public float explosionForce = 10f;
    public float explosionRadius = 5f;
    public GameObject healthBarPrefab;
    public GameObject hitEffectPrefab;

    private AudioSource _audioSource;
    private Rigidbody _rigidbody;
    private float currentHitPoints;
    private Slider healthBar;
    private GameObject healthBarInstance;

    void Start()
    {
        initialRotationForceVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddTorque(initialRotationForceVector * RotationIntensity, ForceMode.Impulse);
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        CreateSphereCollider();
        InitializeHealthBar();
        currentHitPoints = maxHitPoints;
    }

    void Update()
    {
        if (healthBarInstance != null)
        {
            healthBarInstance.transform.LookAt(Camera.main.transform);
        }
    }

    public void ReduceHitPoints(float damage)
    {
        currentHitPoints -= damage;
        UpdateHealthBar();
        ShowHitEffect();
        PlayHitSound();

        if (currentHitPoints <= 0)
        {
            CreateDustCloudExplosion();
            IncrementQuestVariable("Asteroids.NumDestroyed", 1);
            Destroy(healthBarInstance);
            Destroy(gameObject);
        }
    }

    void CreateDustCloudExplosion()
    {
        // ... (keep your existing CreateDustCloudExplosion code)
    }

    void IncrementQuestVariable(string variableName, int incrementAmount)
    {
        // ... (keep your existing IncrementQuestVariable code)
    }

    private void CreateSphereCollider()
    {
        // ... (keep your existing CreateSphereCollider code)
    }

    private void InitializeHealthBar()
    {
        if (healthBarPrefab != null)
        {
            healthBarInstance = Instantiate(healthBarPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
            healthBarInstance.transform.SetParent(transform);
            healthBar = healthBarInstance.GetComponentInChildren<Slider>();
            if (healthBar != null)
            {
                healthBar.maxValue = maxHitPoints;
                healthBar.value = maxHitPoints;
            }
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHitPoints;
        }
    }

    private void ShowHitEffect()
    {
        if (hitEffectPrefab != null)
        {
            GameObject hitEffect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(hitEffect, 1f); // Destroy the effect after 1 second
        }
    }

    private void PlayHitSound()
    {
        if (_audioSource != null && hitSound != null)
        {
            _audioSource.PlayOneShot(hitSound);
        }
    }
}
