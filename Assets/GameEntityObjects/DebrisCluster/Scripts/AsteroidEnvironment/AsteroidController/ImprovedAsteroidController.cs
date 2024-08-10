using System.Collections;
using Dialogue;
using UnityEngine;
using Quests;
using UnityEngine.UI;

public class ImprovedAsteroidController : MonoBehaviour
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
    private float _currentHitPoints;
    private Slider healthBarSlider;
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
        _currentHitPoints = maxHitPoints;
    }

    void Update()
    {
        if (healthBarInstance != null)
        {
            healthBarInstance.transform.rotation = Camera.main.transform.rotation;
        }
    }

    public void ReduceHitPoints(float damage)
    {
        _currentHitPoints -= damage;
        UpdateHealthBar();
        ShowHitEffect();
        PlayHitSound();

        if (_currentHitPoints <= 0)
        {
            CreateDustCloudExplosion();
            Destroy(healthBarInstance);
            Destroy(gameObject);
        }
    }

    void CreateDustCloudExplosion()
    {
        if (dustCloudPrefab != null)
        {
            GameObject dustCloudInstance = Instantiate(dustCloudPrefab, transform.position, Quaternion.identity);

            // Play sound on the dust cloud instance
            if (dustCloudBurstSound != null)
            {
                AudioSource explosionAudio = dustCloudInstance.AddComponent<AudioSource>();
                explosionAudio.clip = dustCloudBurstSound;
                explosionAudio.Play();
            }

            // Add explosion force to the dust particles
            ParticleSystem particleSystem = dustCloudInstance.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                ParticleSystem.MainModule main = particleSystem.main;
                main.startSpeed = explosionForce;
                main.startLifetime = dustCloudLifetime;
            }

            // Destroy the dust cloud after the lifetime
            Destroy(dustCloudInstance, Mathf.Max(dustCloudLifetime, dustCloudBurstSound.length));
        }
    }


    private void CreateSphereCollider()
    {
        sphereColliderObject = gameObject.AddComponent<SphereCollider>();
        sphereColliderObject.radius = 100f;
        sphereColliderObject.isTrigger = true;
        sphereColliderObject.center = Vector3.zero;
    }

    private void InitializeHealthBar()
    {
        if (healthBarPrefab != null)
        {
            healthBarInstance = Instantiate(healthBarPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
            healthBarInstance.transform.SetParent(transform);
            healthBarSlider = healthBarInstance.GetComponentInChildren<Slider>();
            if (healthBarSlider != null)
            {
                healthBarSlider.maxValue = maxHitPoints;
                healthBarSlider.value = maxHitPoints;
            }
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarSlider != null)
        {
            healthBarSlider.value = _currentHitPoints;
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
