using System.Collections;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class BasicAsteroidController : MonoBehaviour
{
    public Vector3 initialRotationForceVector;
    public float RotationIntensity = 100;
    public GameObject dustCloudPrefab;
    public AudioClip dustCloudBurstSound;
    public float hitPoints = 100f;
    public SphereCollider sphereColliderObject;
    public float dustCloudLifetime = 5f;
    public float explosionForce = 10f;
    public float explosionRadius = 5f;

    private AudioSource _audioSource;
    private Rigidbody _rigidbody;

    void Start()
    {
        initialRotationForceVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddTorque(initialRotationForceVector * RotationIntensity, ForceMode.Impulse);
        _audioSource = GetComponent<AudioSource>();

        CreateSphereCollider();
    }

    public void ReduceHitPoints(float damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            CreateDustCloudExplosion();
            IncrementQuestVariable("Asteroids.NumDestroyed", 1);
            Destroy(gameObject);
        }
    }

    void CreateDustCloudExplosion()
    {
        if (dustCloudPrefab != null)
        {
            GameObject dustCloudInstance = Instantiate(dustCloudPrefab, transform.position, Quaternion.identity);

            // Play sound
            if (_audioSource != null && dustCloudBurstSound != null)
            {
                _audioSource.PlayOneShot(dustCloudBurstSound, 1f);
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
            Destroy(dustCloudInstance, dustCloudLifetime);
        }
    }

    void IncrementQuestVariable(string variableName, int incrementAmount)
    {
        int currentValue = DialogueLua.GetVariable(variableName).asInt;
        Debug.Log($"Current value of {variableName}: {currentValue}");

        int newValue = currentValue + incrementAmount;
        DialogueLua.SetVariable(variableName, newValue);

        int verifyValue = DialogueLua.GetVariable(variableName).asInt;
        Debug.Log($"New value of {variableName}: {verifyValue}");

        DialogueManager.Instance.SendUpdateTracker();
    }

    private void CreateSphereCollider()
    {
        sphereColliderObject = gameObject.AddComponent<SphereCollider>();
        sphereColliderObject.radius = 100f;
        sphereColliderObject.isTrigger = true;
        sphereColliderObject.center = Vector3.zero;
    }
}
