using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Audio
{
    public class EngineAudioManager : MonoBehaviour
    {
        [Header("Audio Sources")] [SerializeField]
        public AudioSource mainEngineAudio;
        [SerializeField] public AudioSource idleEngineAudio;
        [SerializeField] public AudioSource afterburnerAudio;
        [SerializeField] public AudioSource fusionDriveAudio;

        [SerializeField] public AudioSource collisionAudio;

        [FormerlySerializedAs("engineStartClip")] [Header("Audio Clips")] [SerializeField]
        public AudioClip boostStartClip;
        [SerializeField] public AudioClip boostEndClunk;
        [SerializeField] public AudioClip engineShutdownClip;
        [SerializeField] public AudioClip collisionMetallic1;
        [SerializeField] public AudioClip collisionMetallic2;
        [SerializeField] public AudioClip fusionDriveClip;
        [SerializeField] public float collisionHitVolumeFactor = 0.1f;

        [Header("Engine Sound Parameters")] [SerializeField]
        private float minEnginePitch = 0.5f;
        [SerializeField] private float maxEnginePitch = 1.5f;
        [SerializeField] private float enginePitchFactor = 0.1f;

        private float _currentSpeed;
        private float _normalizedSpeed;

        private bool _isEngineRunning;
        private bool _isAfterburnerActive;

        private void Start()
        {
            SetupEngineSounds();
        }

        private void SetupEngineSounds()
        {
            mainEngineAudio.loop = true;
            idleEngineAudio.loop = true;
            afterburnerAudio.loop = true;

            idleEngineAudio.Play();
        }


        public void UpdateEngineSounds(float speed, float maxSpeed, bool isThrusting, bool hasActiveInput, bool hasFuel
        )
        {
            _currentSpeed = speed;
            _normalizedSpeed = Mathf.Clamp01(_currentSpeed / maxSpeed);

            UpdateAttitudeEngine(isThrusting && hasFuel);
            UpdateTorchEngine(_isAfterburnerActive && hasFuel);
            UpdateIdleEngine();
            UpdateAfterburner(hasActiveInput && hasFuel);
        }

        private void UpdateAttitudeEngine(bool shouldBeRunning)
        {
            if (shouldBeRunning && !_isEngineRunning)
            {
                StartEngine();
            }
            else if (!shouldBeRunning && _isEngineRunning)
            {
                StopEngine();
            }

            if (_isEngineRunning)
            {
                float pitch = Mathf.Lerp(minEnginePitch, maxEnginePitch, _normalizedSpeed);
                mainEngineAudio.pitch = pitch;
                mainEngineAudio.volume = Mathf.Lerp(0.5f, 1f, _normalizedSpeed);
            }
        }

        private void UpdateTorchEngine(bool shouldBeRunning)
        {
            if (shouldBeRunning && !_isEngineRunning)
            {
                StartTorch();
            }
            else if (!shouldBeRunning && _isEngineRunning)
            {
                StopTorch();
            }

            if (_isEngineRunning)
            {
                float pitch = Mathf.Lerp(minEnginePitch, maxEnginePitch, _normalizedSpeed);
                mainEngineAudio.pitch = pitch;
                mainEngineAudio.volume = Mathf.Lerp(0.5f, 1f, _normalizedSpeed);
            }
        }


        private void StartTorch()
        {
            mainEngineAudio.PlayOneShot(boostStartClip);
            fusionDriveAudio.Play();
            _isEngineRunning = true;
        }

        private void StopTorch()
        {
            fusionDriveAudio.Stop();
            mainEngineAudio.PlayOneShot(boostEndClunk);
            _isEngineRunning = false;
        }

        private void StartEngine()
        {
            mainEngineAudio.PlayOneShot(boostStartClip);
            mainEngineAudio.Play();
            _isEngineRunning = true;
        }

        private void StopEngine()
        {
            mainEngineAudio.Stop();
            mainEngineAudio.PlayOneShot(engineShutdownClip);
            _isEngineRunning = false;
        }

        public void PlayCollisionHit(float linearVelocity)
        {
            collisionAudio.Stop();
            collisionAudio.PlayOneShot(collisionMetallic1, linearVelocity * collisionHitVolumeFactor);

            if (Random.value > 0.5f)
            {
                collisionAudio.PlayOneShot(collisionMetallic2, linearVelocity * collisionHitVolumeFactor);
            }
        }

        private void UpdateIdleEngine()
        {
            idleEngineAudio.volume = Mathf.Lerp(0.8f, 0.2f, _normalizedSpeed);
        }

        private void UpdateAfterburner(bool shouldBeActive)
        {
            if (shouldBeActive && !_isAfterburnerActive)
            {
                afterburnerAudio.Play();
                _isAfterburnerActive = true;
            }

            if (_isAfterburnerActive)
            {
                afterburnerAudio.volume = shouldBeActive
                    ? Mathf.Lerp(0f, 1f, _normalizedSpeed)
                    : Mathf.Lerp(afterburnerAudio.volume, 0f, Time.deltaTime * 2f);

                if (afterburnerAudio.volume < 0.01f)
                {
                    afterburnerAudio.Stop();
                    _isAfterburnerActive = false;
                }
            }
        }
    }
}
