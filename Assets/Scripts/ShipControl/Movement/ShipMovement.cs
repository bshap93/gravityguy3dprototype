using System.Collections;
using System.Collections.Generic;
using Player.Audio;
using Player.InGameResources;
using ShipControl;
using ShipControl.Movement;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.PlayerController.Components
{
    public class ShipMovement : MonoBehaviour
    {
        [FormerlySerializedAs("_playerRb")] public Rigidbody playerRb;
        [SerializeField] SpaceShipController spaceShipController;
        [SerializeField] GameObject spaceShip;

        [SerializeField] EngineAudioManager engineAudioManager;

        [SerializeField] float accelerationFactor = 0.3f;
        [SerializeField] float rotationSpeed = 0.1f;
        [SerializeField] float brakingFactor = 0.01f;
        float _originalPlayerThrusterVolume;

        [SerializeField] float thrustPowerInNewtons = 1000f; // 1 kN of thrust
        [SerializeField] float specificImpulseInSeconds = 1000000f; // Very high for fusion propulsion
        [SerializeField] bool isFusionDriveActive;

        [SerializeField] FuelSystem fuelSystem;

        [SerializeField] float brakingFuelConsumptionFactor = 0.1f;


        public void Start()
        {
            _originalPlayerThrusterVolume = engineAudioManager.mainEngineAudio.volume;


            if (fuelSystem == null)
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
        }

        public void UpdateMovement(float verticalInput, float horizontalInput, bool isBraking, float currentVelocity)
        {
            if (isBraking)
            {
                ApplyBraking();
            }
            else
            {
                ApplyThrust(verticalInput, currentVelocity);
                ApplyTorchThrust();
                ApplyRotationalThrust(horizontalInput);
            }
        }


        public void Initialize(Rigidbody shipRigidbody)
        {
            playerRb = shipRigidbody;
        }

        // Poll for input and apply thrust to the player
        // ReSharper disable Unity.PerformanceAnalysis
        public void ApplyThrust(float verticalInput, float currentVelocity)
        {
            if (verticalInput > 0 && fuelSystem.HasFuel())
            {
                ApplyForwardThrust(ThrustType.AttitudeJet);
                spaceShipController.ThrustForward(ThrustType.AttitudeJet);
                // cwThrusterCone.SetActive(true);
            }
            else if (verticalInput < 0) // Reverse thrust
            {
                ApplyForwardThrust(ThrustType.AttitudeJet, -1);
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

        public void ApplyTorchThrust()
        {
            if (Input.GetKey(KeyCode.LeftShift)) // Torch thrust
            {
                ApplyForwardThrust(ThrustType.Torch);
                spaceShipController.ThrustForward(ThrustType.Torch);
            }
            else
            {
                FadeOutAudio(engineAudioManager.afterburnerAudio, 1, 0.1f);
                spaceShipController.EndThrusterForward();
            }
        }

        // Poll for input and apply rotational thrust to the player
        // ReSharper disable Unity.PerformanceAnalysis
        public void ApplyRotationalThrust(float horizontalInput)
        {
            if (horizontalInput > 0)
            {
                playerRb.AddTorque(Vector3.up * (horizontalInput * rotationSpeed), ForceMode.Impulse);

                PlaySoundAtVolume(engineAudioManager.idleEngineAudio, 1f);

                spaceShipController.ThrustRight();
            }
            else if (horizontalInput < 0)
            {
                playerRb.AddTorque(Vector3.up * (horizontalInput * rotationSpeed), ForceMode.Impulse);

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
                if (playerRb.velocity.magnitude > 0) playerRb.velocity -= playerRb.velocity * brakingFactor;
                // _playerRb.totalTorque -= _playerRb.totalTorque * 0.01f;
                if (playerRb.angularVelocity.magnitude > 0)
                    playerRb.angularVelocity -= playerRb.angularVelocity * brakingFactor;
                else if (playerRb.angularVelocity.magnitude < 0)
                    playerRb.angularVelocity += playerRb.angularVelocity * brakingFactor;

                if (engineAudioManager.mainEngineAudio.isPlaying == false)
                    engineAudioManager.mainEngineAudio.Play();

                fuelSystem.ConsumeFuel(brakingFuelConsumptionFactor);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        void ApplyForwardThrust(ThrustType thrustType, int reversed = 1)
        {
            if (thrustType == ThrustType.AttitudeJet)
            {
                float availableEnergyInJoules = fuelSystem.GetAvailableEnergyInJoules();
                float maxThrustDuration = availableEnergyInJoules / (thrustPowerInNewtons * specificImpulseInSeconds);
                float thrustDuration = Mathf.Min(Time.deltaTime, maxThrustDuration);

                if (thrustDuration > 0)
                {
                    PlaySoundAtVolume(engineAudioManager.mainEngineAudio, _originalPlayerThrusterVolume);
                    if (reversed == 1)
                        spaceShipController.ThrustForward(ThrustType.AttitudeJet);
                    else
                        spaceShipController.ThrustBackward();


                    Vector3 thrustForce = transform.forward *
                                          (thrustPowerInNewtons * thrustDuration * accelerationFactor) *
                                          reversed;

                    playerRb.AddForce(thrustForce, ForceMode.Impulse);

                    float fuelConsumedInGrams = (thrustPowerInNewtons * thrustDuration) /
                        (specificImpulseInSeconds * 9.81f) * 1000f;

                    fuelSystem.ConsumeFuel(fuelConsumedInGrams);
                }
            }
            else if (thrustType == ThrustType.Torch)
            {
                float availableEnergyInJoules = fuelSystem.GetAvailableEnergyInJoules();
                float maxThrustDuration = availableEnergyInJoules / (thrustPowerInNewtons * specificImpulseInSeconds);
                float thrustDuration = Mathf.Min(Time.deltaTime, maxThrustDuration);

                if (thrustDuration > 0)
                {
                    PlaySoundAtVolume(engineAudioManager.afterburnerAudio, _originalPlayerThrusterVolume);
                    if (reversed == 1)
                        spaceShipController.ThrustForward(ThrustType.Torch);


                    Vector3 thrustForce = transform.forward *
                                          (thrustPowerInNewtons * thrustDuration * accelerationFactor) * 1.5f
                        ;

                    playerRb.AddForce(thrustForce, ForceMode.Impulse);

                    float fuelConsumedInGrams = (thrustPowerInNewtons * thrustDuration) /
                                                (specificImpulseInSeconds * 9.81f);

                    fuelSystem.ConsumeFuel(fuelConsumedInGrams);
                }
            }
        }

        public void ResetVelocityAfterTravel()
        {
            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
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
