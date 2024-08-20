using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Player.Audio;
using Player.Resources;
using ShipControl;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        [SerializeField] private EngineAudioManager engineAudioManager;
        [SerializeField] private float maxSpeed = 100f; // Adjust based on your game's scale

        public Transform attachmentPoint;

        [SerializeField] private float accelerationFactor = 0.3f;
        [SerializeField] private float rotationSpeed = 0.1f;
        [SerializeField] private float brakingFactor = 0.01f;

        public Attachment currentAttachment;


        public GameObject identificationTextObject;
        public CinemachineFreeLook playerFreeLookCamera;


        public Transform cameraTransform;

        GameObject _focalPoint;


        float _horizontalInput;
        float _originalPlayerRotationVolume;

        float _originalPlayerThrusterVolume;


        Rigidbody _playerRb;

        float _verticalInput;

        float _originalFreeLookXMaxSpeed;
        float _originalFreeLookYMaxSpeed;

        [FormerlySerializedAs("firstAstroidInRange")]
        public List<GameObject> targetsInRange = new List<GameObject>();

        [SerializeField] FuelSystem fuelSystem;
        [SerializeField] float thrustPowerInNewtons = 1000f; // 1 kN of thrust
        [SerializeField] float specificImpulseInSeconds = 1000000f; // Very high for fusion propulsion

        [SerializeField] float flipTorque = 10f;
        [SerializeField] float minVelocityForFlip = 5f;
        [SerializeField] float alignmentThreshold = 0.95f;
        Vector3 _flipTarget;

        public GameObject spaceShip;
        [SerializeField] SpaceShipController spaceShipController;
        [FormerlySerializedAs("CWThrusterCone")] [SerializeField]
        GameObject cwThrusterCone;
        void Start()
        {
            _playerRb = GetComponent<Rigidbody>();

            // AssignAudioSources();

            // _originalPlayerThrusterVolume = _thrustAudioSource.volume;


            _originalFreeLookXMaxSpeed = playerFreeLookCamera.m_XAxis.m_MaxSpeed;
            _originalFreeLookYMaxSpeed = playerFreeLookCamera.m_YAxis.m_MaxSpeed;

            if (fuelSystem == null)
            {
                fuelSystem = GetComponent<FuelSystem>();
            }

            if (fuelSystem == null)
            {
                Debug.LogError("FuelSystem not found on the player ship!");
            }

            spaceShipController = spaceShip.GetComponent<SpaceShipController>();
            cwThrusterCone = spaceShipController.thruster;

            if (engineAudioManager == null)
            {
                engineAudioManager = GetComponent<EngineAudioManager>();
            }

            if (engineAudioManager == null)
            {
                Debug.LogError("EngineAudioManager not found on the player ship!");
            }
        }
        // Update is called once per frame
        void Update()
        {
            LockCameraRotation();
            UpdateEngineAudio();
        }

        void FixedUpdate()
        {
            ApplyThrust();
            ApplyRotationalThrust();
            ApplyBraking();
        }


        public void AttachUpgrade(Attachment attachmentPrefab)
        {
            if (currentAttachment != null)
            {
                Destroy(currentAttachment.gameObject);
            }

            currentAttachment = Instantiate(
                attachmentPrefab, attachmentPoint.position, attachmentPoint.rotation, attachmentPoint);

            Debug.Log($"{currentAttachment.attachmentName} attached to the ship. {currentAttachment.description}");
        }

        public void UseAttachment()
        {
            if (currentAttachment != null)
            {
                currentAttachment.Activate();
            }
            else
            {
                Debug.Log("No attachment equipped!");
            }
        }


        public void StopUsingAttachment()
        {
            if (currentAttachment != null)
            {
                currentAttachment.Deactivate();
            }
        }


        private void LockCameraRotation()
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                playerFreeLookCamera.m_YAxis.m_MaxSpeed = 0;
                playerFreeLookCamera.m_XAxis.m_MaxSpeed = 0;
            }
            else
            {
                playerFreeLookCamera.m_YAxis.m_MaxSpeed = _originalFreeLookYMaxSpeed;
                playerFreeLookCamera.m_XAxis.m_MaxSpeed = _originalFreeLookXMaxSpeed;
            }
        }

        void UpdateEngineAudio()
        {
            float currentSpeed = _playerRb.velocity.magnitude;
            bool isThrusting = _verticalInput != 0;
            bool hasActiveInput = isThrusting || Input.GetKey(KeyCode.LeftShift);
            bool hasFuel = fuelSystem.HasFuel();

            engineAudioManager.UpdateEngineSounds(currentSpeed, maxSpeed, isThrusting, hasActiveInput, hasFuel);
        }


        // Poll for input and apply thrust to the player
        // ReSharper disable Unity.PerformanceAnalysis
        void ApplyThrust()
        {
            _verticalInput = Input.GetAxis("Vertical");


            if (_verticalInput > 0 && fuelSystem.HasFuel())
            {
                ApplyForwardThrust();
                spaceShipController.ThrustForward();
                // cwThrusterCone.SetActive(true);
            }
            else if (_verticalInput < 0) // Reverse thrust
            {
                ApplyForwardThrust(-1);
                cwThrusterCone.SetActive(false);
                spaceShipController.ThrustBackward();
            }

            else
            {
                FadeOutAudio(engineAudioManager.mainEngineAudio, 1, 0.1f);
                cwThrusterCone.SetActive(false);
                spaceShipController.EndThrusterForward();
                spaceShipController.EndThrusterBackward();
            }

            Quaternion.Euler(0.0f, cameraTransform.eulerAngles.y, 0.0f);
        }
        // ReSharper disable Unity.PerformanceAnalysis
        void ApplyForwardThrust(int reversed = 1)
        {
            float availableEnergyInJoules = fuelSystem.GetAvailableEnergyInJoules();
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

                fuelSystem.ConsumeFuel(fuelConsumedInGrams);
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
        void FadeOutAudio(AudioSource audioSource, float fadeDuration, float endVolume = 0f)
        {
            if (!audioSource.isPlaying) return;
            StartCoroutine(FadeOutCoroutine(audioSource, fadeDuration, endVolume));
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

        // Poll for input and apply braking to the player
        // ReSharper disable Unity.PerformanceAnalysis
        void ApplyBraking()
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

                fuelSystem.ConsumeFuel(0.1f);
            }
        }


        // Poll for input and apply rotational thrust to the player
        // ReSharper disable Unity.PerformanceAnalysis
        void ApplyRotationalThrust()
        {
            _horizontalInput = Input.GetAxis("Horizontal");


            if (_horizontalInput > 0)
            {
                _playerRb.AddTorque(Vector3.up * (_horizontalInput * rotationSpeed), ForceMode.Impulse);

                PlaySoundAtVolume(engineAudioManager.idleEngineAudio, 1f);

                spaceShipController.ThrustRight();
            }
            else if (_horizontalInput < 0)
            {
                _playerRb.AddTorque(Vector3.up * (_horizontalInput * rotationSpeed), ForceMode.Impulse);

                PlaySoundAtVolume(engineAudioManager.idleEngineAudio, 1f);

                spaceShipController.ThrustLeft();
            }
            else if (_horizontalInput == 0)
            {
                FadeOutAudio(engineAudioManager.idleEngineAudio, 1f);
                spaceShipController.EndThrusterLeft();
                spaceShipController.EndThrusterRight();
            }
        }

        public void RefuelShip(float amount)
        {
            fuelSystem.RefuelShip(amount);
        }
    }
}
