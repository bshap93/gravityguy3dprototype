using UnityEngine;
using UnityEngine.Serialization;

namespace GameEntityObjects.DebrisCluster.Scripts.AsteroidEnvironment.AsteroidController
{
    public class BasicAsteroidController : MonoBehaviour
    {
        public float miningResistance = 1f; // New variable to control how hard the asteroid is to mine

        public Vector3 initialRotationForceVector;
        [FormerlySerializedAs("RotationIntensity")]
        public float rotationIntensity = 100;
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
            initialRotationForceVector = new Vector3(
                Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.AddTorque(initialRotationForceVector * rotationIntensity, ForceMode.Impulse);
            _audioSource = GetComponent<AudioSource>();
            if (_audioSource == null)
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }

            CreateSphereCollider();
        }

        public void Mine(float miningPower)
        {
            float damage = miningPower / miningResistance;
            ReduceHitPoints(damage);
        }

        public void ReduceHitPoints(float damage)
        {
            hitPoints -= damage;
            if (hitPoints <= 0)
            {
                CreateDustCloudExplosion();
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
                ParticleSystem particleComponent = dustCloudInstance.GetComponent<ParticleSystem>();
                if (particleComponent != null)
                {
                    ParticleSystem.MainModule main = particleComponent.main;
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
            sphereColliderObject.radius = 30f;
            sphereColliderObject.isTrigger = true;
            sphereColliderObject.center = Vector3.zero;
        }
    }
}
