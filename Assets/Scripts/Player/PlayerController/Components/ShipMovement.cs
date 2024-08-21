using System.Collections;
using System.Collections.Generic;
using Player.Audio;
using Player.Resources;
using ShipControl;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.PlayerController.Components
{
    public class ShipMovement : MonoBehaviour
    {
        private Rigidbody _playerRb;
        [SerializeField] SpaceShipController spaceShipController;
        [SerializeField] GameObject spaceShip;

        [SerializeField] EngineAudioManager engineAudioManager;
        FuelSystem _fuelSystem;

        [SerializeField] float accelerationFactor = 0.3f;
        [SerializeField] float rotationSpeed = 0.1f;
        [SerializeField] float brakingFactor = 0.01f;
        float _originalPlayerThrusterVolume;

        [SerializeField] float thrustPowerInNewtons = 1000f; // 1 kN of thrust
        [SerializeField] float specificImpulseInSeconds = 1000000f; // Very high for fusion propulsion


        public void Start()
        {
            _originalPlayerThrusterVolume = engineAudioManager.mainEngineAudio.volume;

            if (_fuelSystem == null)
            {
                _fuelSystem = GetComponent<FuelSystem>();
            }

            if (_fuelSystem == null)
            {
                Debug.LogError("FuelSystem not found on the player ship!");
            }

            spaceShipController = spaceShip.GetComponent<SpaceShipController>();

            if (engineAudioManager == null)
            {
                engineAudioManager = GetComponent<EngineAudioManager>();
            }

            if (engineAudioManager == null)
            {
                Debug.LogError("EngineAudioManager not found on the player ship!");
            }


            _fuelSystem = GetComponent<FuelSystem>();
        }


        public void Initialize(Rigidbody shipRigidbody)
        {
            _playerRb = shipRigidbody;
        }

        // Poll for input and apply thrust to the player
        // ReSharper disable Unity.PerformanceAnalysis
        public void ApplyThrust(float verticalInput)
        {
            if (verticalInput > 0 && _fuelSystem.HasFuel())
            {
                ApplyForwardThrust();
                spaceShipController.ThrustForward();
                // cwThrusterCone.SetActive(true);
            }
            else if (verticalInput < 0) // Reverse thrust
            {
                ApplyForwardThrust(-1);
                spaceShipController.mainFusionThruster.SetActive(false);
                spaceShipController.ThrustBackward();
            }

            else
            {
                FadeOutAudio(engineAudioManager.mainEngineAudio, 1, 0.1f);
                spaceShipController.mainFusionThruster.SetActive(false);
                spaceShipController.EndThrusterForward();
                spaceShipController.EndThrusterBackward();
            }
        }

        // Poll for input and apply rotational thrust to the player
        // ReSharper disable Unity.PerformanceAnalysis
        public void ApplyRotationalThrust(float horizontalInput)
        {
            if (horizontalInput > 0)
            {
                _playerRb.AddTorque(Vector3.up * (horizontalInput * rotationSpeed), ForceMode.Impulse);

                PlaySoundAtVolume(engineAudioManager.idleEngineAudio, 1f);

                spaceShipController.ThrustRight();
            }
            else if (horizontalInput < 0)
            {
                _playerRb.AddTorque(Vector3.up * (horizontalInput * rotationSpeed), ForceMode.Impulse);

                PlaySoundAtVolume(engineAudioManager.idleEngineAudio, 1f);

                spaceShipController.ThrustLeft();
            }
            else if (horizontalInput == 0)
            {
                FadeOutAudio(engineAudioManager.idleEngineAudio, 1f);
                spaceShipController.EndThrusterLeft();
                spaceShipController.EndThrusterRight();
            }
        }

        static IEnumerator FadeOutCoroutine(AudioSource audioSource, float fadeDuration, float endVolume)
        {
            float startVolume = audioSource.volume;
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float newVolume = Mathf.Lerp(startVolume, endVolume, elapsedTime / fadeDuration);
                audioSource.volume = newVolume;
                yield return null;
            }

            if (endVolume == 0f)
            {
                audioSource.Stop();
            }
        }

        void FadeOutAudio(AudioSource audioSource, float fadeDuration, float endVolume = 0f)
        {
            if (!audioSource.isPlaying) return;
            StartCoroutine(FadeOutCoroutine(audioSource, fadeDuration, endVolume));
        }

        // Poll for input and apply braking to the player
        // ReSharper disable Unity.PerformanceAnalysis
        public void ApplyBraking()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                spaceShipController.HandleBrakingThrusters();
                spaceShipController.HandleBrakingAngularThrusters();

                PlaySoundAtVolume(engineAudioManager.mainEngineAudio, _originalPlayerThrusterVolume);


                // Braking logic here
                if (_playerRb.velocity.magnitude > 0) _playerRb.velocity -= _playerRb.velocity * brakingFactor;
                // _playerRb.totalTorque -= _playerRb.totalTorque * 0.01f;
                if (_playerRb.angularVelocity.magnitude > 0)
                    _playerRb.angularVelocity -= _playerRb.angularVelocity * brakingFactor;
                else if (_playerRb.angularVelocity.magnitude < 0)
                    _playerRb.angularVelocity += _playerRb.angularVelocity * brakingFactor;

                if (engineAudioManager.mainEngineAudio.isPlaying == false)
                    engineAudioManager.mainEngineAudio.Play();

                _fuelSystem.ConsumeFuel(0.1f);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void ApplyForwardThrust(int reversed = 1)
        {
            float availableEnergyInJoules = _fuelSystem.GetAvailableEnergyInJoules();
            float maxThrustDuration = availableEnergyInJoules / (thrustPowerInNewtons * specificImpulseInSeconds);
            float thrustDuration = Mathf.Min(Time.deltaTime, maxThrustDuration);

            if (thrustDuration > 0)
            {
                PlaySoundAtVolume(engineAudioManager.mainEngineAudio, _originalPlayerThrusterVolume);
                if (reversed == 1)
                    spaceShipController.ThrustForward();
                else
                    spaceShipController.ThrustBackward();


                Vector3 thrustForce = transform.forward * (thrustPowerInNewtons * thrustDuration * accelerationFactor) *
                                      reversed;

                _playerRb.AddForce(thrustForce, ForceMode.Impulse);

                float fuelConsumedInGrams = (thrustPowerInNewtons * thrustDuration) /
                    (specificImpulseInSeconds * 9.81f) * 1000f;

                _fuelSystem.ConsumeFuel(fuelConsumedInGrams);
            }
        }


        void PlaySoundAtVolume(AudioSource audioSource, float volume)
        {
            if (audioSource.isPlaying == false)
            {
                audioSource.volume = volume;
                audioSource.Play();
            }
        }
    }
}
