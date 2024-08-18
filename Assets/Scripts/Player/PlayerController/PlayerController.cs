using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Player.Resources;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        public Transform attachmentPoint;

        [SerializeField] private float accelerationFactor = 0.3f;
        [SerializeField] private float rotationSpeed = 0.1f;
        [SerializeField] private float brakingFactor = 0.01f;

        public Attachment currentAttachment;


        public GameObject identificationTextObject;
        public CinemachineFreeLook playerFreeLookCamera;

        public Animator[] thrusterAnimator;

        public Transform cameraTransform;

        [FormerlySerializedAs("rotationalFactor")]
        GameObject _focalPoint;

        public EventManager eventManager;


        float _horizontalInput;
        float _originalPlayerRotationVolume;

        float _originalPlayerThrusterVolume;


        Rigidbody _playerRb;
        AudioSource _rotateAudioSource;

        AudioSource _thrustAudioSource;
        float _verticalInput;

        float _originalFreeLookXMaxSpeed;
        float _originalFreeLookYMaxSpeed;

        [FormerlySerializedAs("firstAstroidInRange")]
        public List<GameObject> targetsInRange = new List<GameObject>();

        [SerializeField] private FuelSystem fuelSystem;
        [SerializeField] private float thrustPowerInNewtons = 1000f; // 1 kN of thrust
        [SerializeField] private float specificImpulseInSeconds = 1000000f; // Very high for fusion propulsion
        static readonly int IsThrusting = Animator.StringToHash("isThrusting");

        [SerializeField] private float flipTorque = 10f;
        [SerializeField] private float minVelocityForFlip = 5f;
        [SerializeField] private float alignmentThreshold = 0.95f;
        private bool _isFlipping = false;
        private Vector3 _flipTarget;

        public GameObject spaceShip;
        [SerializeField] SpaceShipController spaceShipController;
        [SerializeField] GameObject CWThrusterCone;


        void Start()
        {
            _playerRb = GetComponent<Rigidbody>();

            AssignAudioSources();

            _originalPlayerThrusterVolume = _thrustAudioSource.volume;


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
            CWThrusterCone = spaceShipController.thruster;
        }
        // Update is called once per frame
        void Update()
        {
            LockCameraRotation();
        }

        void FixedUpdate()
        {
            if (!_isFlipping)
            {
                ApplyThrust();
                ApplyRotationalThrust();
                ApplyBraking();
            }
            else
            {
                ContinueFlipAndBurn();
            }
        }
        void AssignAudioSources()
        {
            _thrustAudioSource = GetComponents<AudioSource>()[0];
            _rotateAudioSource = GetComponents<AudioSource>()[1];
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


        // Poll for input and apply thrust to the player
        void ApplyThrust()
        {
            _verticalInput = Input.GetAxis("Vertical");


            if (_verticalInput > 0 && fuelSystem.HasFuel())
            {
                ApplyForwardThrust();
                CWThrusterCone.SetActive(true);
            }
            else if (_verticalInput < 0 && !_isFlipping) // Reverse thrust
            {
                StartFlipAndBurn();
                CWThrusterCone.SetActive(false);
            }

            else
            {
                if (thrusterAnimator[0].GetBool(IsThrusting))
                {
                    foreach (var t in thrusterAnimator)
                        t.SetBool(IsThrusting, false);
                }

                NoisePeterOff(_thrustAudioSource, 1, 0.9f);
                CWThrusterCone.SetActive(false);
            }

            Quaternion.Euler(0.0f, cameraTransform.eulerAngles.y, 0.0f);
        }
        void ApplyForwardThrust(bool reversed = false)
        {
            float availableEnergyInJoules = fuelSystem.GetAvailableEnergyInJoules();
            float maxThrustDuration = availableEnergyInJoules / (thrustPowerInNewtons * specificImpulseInSeconds);
            float thrustDuration = Mathf.Min(Time.deltaTime, maxThrustDuration);

            if (thrustDuration > 0)
            {
                if (!reversed)
                    if (thrusterAnimator[0].GetBool(IsThrusting) == false)
                    {
                        for (var i = 0; i < thrusterAnimator.Length; i++)
                            thrusterAnimator[i].SetBool(IsThrusting, true);
                    }

                PlaySoundAtVolume(_thrustAudioSource, _originalPlayerThrusterVolume);

                Vector3 thrustForce = transform.forward * (thrustPowerInNewtons * thrustDuration * accelerationFactor);
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
        void NoisePeterOff(AudioSource audioSource, int lengthSeconds, float volumeDecay)
        {
            if (audioSource.isPlaying)
            {
                StartCoroutine(SoundActionDuration(audioSource, lengthSeconds));
                // Gradually reduce the volume of the audio source
                var volume = audioSource.volume;
                audioSource.volume *= volumeDecay;
            }
        }

        IEnumerator SoundActionDuration(AudioSource audioSource, int lengthSeconds)
        {
            yield return new WaitForSeconds(lengthSeconds);


            audioSource.Stop();
        }

        // Poll for input and apply braking to the player
        void ApplyBraking()
        {
            if (Input.GetKey(KeyCode.Space) || _isFlipping)
            {
                // Braking logic here
                if (_playerRb.velocity.magnitude > 0) _playerRb.velocity -= _playerRb.velocity * brakingFactor;
                // _playerRb.totalTorque -= _playerRb.totalTorque * 0.01f;
                if (_playerRb.angularVelocity.magnitude > 0)
                    _playerRb.angularVelocity -= _playerRb.angularVelocity * brakingFactor;
                else if (_playerRb.angularVelocity.magnitude < 0)
                    _playerRb.angularVelocity += _playerRb.angularVelocity * brakingFactor;

                if (_thrustAudioSource.isPlaying == false) _thrustAudioSource.Play();
                fuelSystem.ConsumeFuel(0.1f);
            }
        }


        // Poll for input and apply rotational thrust to the player
        void ApplyRotationalThrust()
        {
            _horizontalInput = Input.GetAxis("Horizontal");


            if (_horizontalInput != 0)
            {
                _playerRb.AddTorque(Vector3.up * (_horizontalInput * rotationSpeed), ForceMode.Impulse);

                PlaySoundAtVolume(_rotateAudioSource, 1f);
            }
            else if (_horizontalInput == 0)
            {
                NoisePeterOff(_rotateAudioSource, 1, 0.9f);
            }
        }

        public void RefuelShip(float amount)
        {
            fuelSystem.RefuelShip(amount);
        }

        void StartFlipAndBurn()
        {
            if (!_isFlipping && _playerRb.velocity.magnitude > minVelocityForFlip)
            {
                _isFlipping = true;
                _flipTarget = -_playerRb.velocity.normalized;

                // Stop any existing rotation
                _playerRb.angularVelocity = Vector3.zero;
            }
        }
        void ContinueFlipAndBurn()
        {
            // Check if we're aligned with the flip target
            float alignment = Vector3.Dot(transform.forward, _flipTarget);

            if (alignment < alignmentThreshold)
            {
                // Calculate the axis of rotation
                Vector3 rotationAxis = Vector3.Cross(transform.forward, _flipTarget).normalized;

                // Apply torque to rotate the ship
                _playerRb.AddTorque(rotationAxis * flipTorque, ForceMode.Force);

                // Visual feedback
                for (var i = 0; i < thrusterAnimator.Length; i++)
                    thrusterAnimator[i].SetBool("isThrusting", false);
            }
            else
            {
                // We're aligned, stop rotating and start reverse thrust
                _playerRb.angularVelocity = Vector3.zero;
                _isFlipping = false;
                ApplyForwardThrust();
                ApplyBraking();
            }
        }
    }
}
