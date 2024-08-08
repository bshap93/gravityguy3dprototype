using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        public Transform attachmentPoint;

        [SerializeField] private float accelerationFactor = 0.3f;
        [SerializeField] private float rotationSpeed = 0.1f;
        [SerializeField] private float projectileRecoil = 20f;

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
        [FormerlySerializedAs("_canDock")] [SerializeField]
        bool canDock = false;
        public int debrisObjectsInRange = 0;

        float _originalFreeLookXMaxSpeed;
        float _originalFreeLookYMaxSpeed;

        [FormerlySerializedAs("firstAstroidInRange")]
        public List<GameObject> targetsInRange = new List<GameObject>();


        void Start()
        {
            _playerRb = GetComponent<Rigidbody>();

            AssignAudioSources();

            _originalPlayerThrusterVolume = _thrustAudioSource.volume;

            GameObject.FindWithTag("PlayerThrustFlame");

            eventManager.CommenceShipDocking.AddListener(AskPlayerToDock);

            _originalFreeLookXMaxSpeed = playerFreeLookCamera.m_XAxis.m_MaxSpeed;
            _originalFreeLookYMaxSpeed = playerFreeLookCamera.m_YAxis.m_MaxSpeed;

            ConsoleManager consoleManager = FindObjectOfType<ConsoleManager>();
            GameLogger.Initialize(consoleManager);
            GameLogger.LogAction("System Initialized");

        }
        // Update is called once per frame
        void Update()
        {
            ApplyThrust();
            ApplyRotationalThrust();
            ApplyBraking();
            LockCameraRotation();
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


        void AskPlayerToDock(string message)
        {
            Debug.Log(message);
            identificationTextObject.SetActive(true);

            var typeWriter = identificationTextObject.GetComponent<UITextTypeWriter>();

            canDock = true;
        }

        void AlertPlayerLeavingAsteroidRange(GameObject asteroid)
        {
            Debug.Log("Asteroid is out of range");
            debrisObjectsInRange--;
            targetsInRange.Remove(asteroid);
        }

        void AlertPlayerOfAsteroidInRange(GameObject asteroid)
        {
            Debug.Log("Asteroid is in range");
            identificationTextObject.SetActive(true);
            var typeWriter = identificationTextObject.GetComponent<UITextTypeWriter>();
            // typeWriter.ChangeText("Asteroid in range");
            debrisObjectsInRange++;
            targetsInRange.Add(asteroid);
        }


        // Poll for input and apply thrust to the player
        void ApplyThrust()
        {
            _verticalInput = Input.GetAxis("Vertical");
            // flames.gameObject.SetActive(_verticalInput > 0);


            // Apply relative force to the player 
            if (_verticalInput > 0)
            {
                if (thrusterAnimator[0].GetBool("isThrusting") == false)
                {
                    Debug.Log("Thrusting up");
                    for (var i = 0; i < thrusterAnimator.Length; i++) thrusterAnimator[i].SetBool("isThrusting", true);
                }

                PlaySoundAtVolume(_thrustAudioSource, _originalPlayerThrusterVolume);

                _playerRb.AddRelativeForce(Vector3.forward * accelerationFactor, ForceMode.Impulse);
            }
            else if (_verticalInput < 0)
            {
                _playerRb.AddRelativeForce(Vector3.back * accelerationFactor, ForceMode.Impulse);
                if (thrusterAnimator[0].GetBool("isThrusting"))
                {
                    Debug.Log("Thrusting down");
                    for (var i = 0; i < thrusterAnimator.Length; i++) thrusterAnimator[i].SetBool("isThrusting", false);
                }

                NoisePeterOff(_thrustAudioSource, 1, 0.9f);
            }
            else
            {
                if (thrusterAnimator[0].GetBool("isThrusting"))
                {
                    for (var i = 0; i < thrusterAnimator.Length; i++) thrusterAnimator[i].SetBool("isThrusting", false);
                }

                NoisePeterOff(_thrustAudioSource, 1, 0.9f);
            }

            Quaternion.Euler(0.0f, cameraTransform.eulerAngles.y, 0.0f);
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
            if (Input.GetKey(KeyCode.Space))
            {
                // Braking logic here
                if (_playerRb.velocity.magnitude > 0) _playerRb.velocity -= _playerRb.velocity * 0.01f;
                // _playerRb.totalTorque -= _playerRb.totalTorque * 0.01f;
                if (_playerRb.angularVelocity.magnitude > 0)
                    _playerRb.angularVelocity -= _playerRb.angularVelocity * 0.01f;
                else if (_playerRb.angularVelocity.magnitude < 0)
                    _playerRb.angularVelocity += _playerRb.angularVelocity * 0.01f;

                if (_thrustAudioSource.isPlaying == false) _thrustAudioSource.Play();
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
    }
}
