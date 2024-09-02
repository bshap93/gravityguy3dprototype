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

        [Header("Audio Clips")] [SerializeField]
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

        #region Initialization

        // Sets up the initial state of the engine sounds
        private void SetupEngineSounds()
        {
            mainEngineAudio.loop = true;
            idleEngineAudio.loop = true;
            afterburnerAudio.loop = true;

            idleEngineAudio.Play();
        }

        #endregion

        #region Public Methods

        // Updates the engine sounds based on the current speed, thrusting state, input activity, and fuel availability
        public void UpdateEngineSounds(float speed, float maxSpeed, bool isThrusting, bool hasActiveInput, bool hasFuel)
        {
            _currentSpeed = speed;
            _normalizedSpeed = Mathf.Clamp01(_currentSpeed / maxSpeed);

            UpdateMainEngine(isThrusting && hasFuel);
            UpdateTorchEngine(isThrusting && hasFuel);
            UpdateIdleEngine();
            UpdateAfterburner(hasActiveInput && hasFuel);
        }

        // Plays collision sound effects based on the impact velocity
        public void PlayCollisionHit(float linearVelocity)
        {
            collisionAudio.Stop();
            collisionAudio.PlayOneShot(collisionMetallic1, linearVelocity * collisionHitVolumeFactor);

            if (Random.value > 0.5f)
            {
                collisionAudio.PlayOneShot(collisionMetallic2, linearVelocity * collisionHitVolumeFactor);
            }
        }

        #endregion

        #region Engine Control

        // Handles the main engine's running state and sound adjustment
        private void UpdateMainEngine(bool shouldBeRunning)
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
                AdjustEnginePitchAndVolume(mainEngineAudio);
            }
        }

        // Starts the main engine sound
        private void StartEngine()
        {
            mainEngineAudio.PlayOneShot(boostStartClip);
            mainEngineAudio.Play();
            _isEngineRunning = true;
        }

        // Stops the main engine sound
        private void StopEngine()
        {
            mainEngineAudio.Stop();
            mainEngineAudio.PlayOneShot(engineShutdownClip);
            _isEngineRunning = false;
        }

        #endregion

        #region Torch Control

        // Handles the torch engine's running state and sound adjustment
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
                AdjustEnginePitchAndVolume(mainEngineAudio);
            }
        }

        // Starts the torch sound
        private void StartTorch()
        {
            mainEngineAudio.PlayOneShot(boostStartClip);
            mainEngineAudio.Play();

            fusionDriveAudio.Play();
            _isEngineRunning = true;
        }

        // Stops the torch sound
        private void StopTorch()
        {
            fusionDriveAudio.Stop();
            mainEngineAudio.Stop();
            mainEngineAudio.PlayOneShot(boostEndClunk);
            _isEngineRunning = false;
        }

        #endregion

        #region Afterburner Control

        // Handles the afterburner sound state and volume adjustment
        private void UpdateAfterburner(bool shouldBeActive)
        {
            if (shouldBeActive && !_isAfterburnerActive)
            {
                afterburnerAudio.Play();
                _isAfterburnerActive = true;
            }

            if (_isAfterburnerActive)
            {
                AdjustAfterburnerVolume(shouldBeActive);
            }
        }

        // Adjusts the afterburner volume based on whether it should be active
        private void AdjustAfterburnerVolume(bool shouldBeActive)
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

        #endregion

        #region Idle Engine Control

        // Adjusts the idle engine volume based on the normalized speed
        private void UpdateIdleEngine()
        {
            idleEngineAudio.volume = Mathf.Lerp(0.8f, 0.2f, _normalizedSpeed);
        }

        #endregion

        #region Utility Methods

        // Adjusts the pitch and volume of the given audio source based on the normalized speed
        private void AdjustEnginePitchAndVolume(AudioSource engineAudio)
        {
            float pitch = Mathf.Lerp(minEnginePitch, maxEnginePitch, _normalizedSpeed);
            engineAudio.pitch = pitch;
            engineAudio.volume = Mathf.Lerp(0.5f, 1f, _normalizedSpeed);
        }

        #endregion
    }
}
