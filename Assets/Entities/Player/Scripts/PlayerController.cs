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
        
        [SerializeField] private float accelerationFactor = 0.3f;
        [SerializeField] private float rotationSpeed = 0.1f;
        [SerializeField] private float projectileRecoil = 20f;
        [SerializeField] private float laserStrength = 1f;
        
        
        
        public GameObject guidingLine;
        public GameObject projectileThruster;
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
        AudioSource _laserAudioSource;

        AudioSource _thrustAudioSource;
        float _verticalInput;
        bool _canDock = false;
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
            var asteroids = GameObject.FindGameObjectsWithTag("Asteroid");
            ListenForTargetInRangeStatusChanges(asteroids);

            _originalFreeLookXMaxSpeed = playerFreeLookCamera.m_XAxis.m_MaxSpeed;
            _originalFreeLookYMaxSpeed = playerFreeLookCamera.m_YAxis.m_MaxSpeed;
        }
        // Update is called once per frame
        void Update()
        {
            ApplyThrust();
            ApplyRotationalThrust();
            ApplyBraking();
            InteractWithObject();
            LockCameraRotation();
        }
        void AssignAudioSources()
        {
            _thrustAudioSource = GetComponents<AudioSource>()[0];
            _rotateAudioSource = GetComponents<AudioSource>()[1];
            _laserAudioSource = GetComponents<AudioSource>()[2];
        }
        void ListenForTargetInRangeStatusChanges(GameObject[] asteroids)
        {
            foreach (GameObject asteroid in asteroids)
            {
                asteroid.GetComponent<AsteroidInRangeController>().TargetInRange
                    .AddListener(AlertPlayerOfAsteroidInRange);
            }

            foreach (GameObject asteroid in asteroids)
            {
                asteroid.GetComponent<AsteroidInRangeController>().TargetOutOfRange
                    .AddListener(AlertPlayerLeavingAsteroidRange);
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
            typeWriter.ChangeText("Press 'X' to dock");
            _canDock = true;
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

        // ReSharper disable Unity.PerformanceAnalysis
        void InteractWithObject()
        {
            if (_canDock && Input.GetKeyDown(KeyCode.X))
            {
                Debug.Log("Docking with capital ship");
                identificationTextObject.SetActive(false);
                _playerRb.velocity = Vector3.zero;
                SceneManager.LoadScene("IndoorScene01", LoadSceneMode.Single);
            }


            if (debrisObjectsInRange > 0 && Input.GetKey(KeyCode.X))
            {
                identificationTextObject.SetActive(false);
                ApplyLaserToDebrisObject();
            }
        }

        void ApplyLaserToDebrisObject()
        {
            if (targetsInRange.Count > 0)
            {
                Vector3 playerTransform = transform.position;
                var targetsInViewAndInRange = GetTargetsInView(targetsInRange);
                // var asteroidToTarget = GetTargetMostCentrallyInView();
                if (targetsInViewAndInRange.Count == 0)
                {
                    var asteroid = targetsInRange.First();
                    HitTargetWithLaser(playerTransform, asteroid);
                }
                else
                {
                    var asteroid = targetsInViewAndInRange.First();
                    HitTargetWithLaser(playerTransform, asteroid);
                }

                ;
            }
        }

        void HitTargetWithLaser(Vector3 playerPosition, GameObject target)
        {
            if (target == null) return;
            Vector3 asteroidPosition = target.transform.position;
            SpawnLaserBetweenPlayerAndTarget(playerPosition, asteroidPosition);

            target.GetComponent<BasicAsteroidController>().ReduceHitPoints(laserStrength);
        }

        List<GameObject> GetTargetsInView(List<GameObject> targets)
        {
            var targetsInView = new List<GameObject>();
            foreach (var target in targets)
            {
                if (target == null) continue;
                var targetInView = target.GetComponent<AsteroidInRangeController>().isInView;
                if (targetInView)
                {
                    targetsInView.Add(target);
                }
            }

            return targetsInView;
        }

        void SpawnLaserBetweenPlayerAndTarget(Vector3 playerPosition, Vector3 asteroidPosition)
        {
            if (debrisObjectsInRange > 0 && Input.GetKeyDown(KeyCode.X))
            {
                _laserAudioSource.Play();
                StartCoroutine(SoundActionDuration(_laserAudioSource, 1));
            }

            GameObject laser = LaserPool.Instance.GetLaser();
            LineRenderer lineRenderer = laser.GetComponent<LineRenderer>();

            lineRenderer.startColor = Color.red;
            lineRenderer.endColor = Color.yellow;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, playerPosition);
            lineRenderer.SetPosition(1, asteroidPosition);

            StartCoroutine(ReturnLaserToPool(laser, 0.2f));
        }

        IEnumerator ReturnLaserToPool(GameObject laser, float delay)
        {
            yield return new WaitForSeconds(delay);
            LaserPool.Instance.ReturnLaser(laser);
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

        void ApplyAttitudeAdjustmentToCamera()
        {
        }


        // Poll for input and apply rotational thrust to the player
        void ApplyRotationalThrust()
        {
            _horizontalInput = Input.GetAxis("Horizontal");


            if (_horizontalInput != 0)
            {
                Debug.Log(_horizontalInput);
                _playerRb.AddTorque(Vector3.up * (_horizontalInput * rotationSpeed), ForceMode.Impulse);

                PlaySoundAtVolume(_rotateAudioSource, 1f);
            }
            else if (_horizontalInput == 0)
            {
                NoisePeterOff(_rotateAudioSource, 1, 0.9f);
            }
        }

        void LineUpShot()
        {
            if (Input.GetKey(KeyCode.Q))
                guidingLine.gameObject.SetActive(true);
            else
                guidingLine.gameObject.SetActive(false);
        }

        void LaunchProjectile()
        {
            if (Input.GetMouseButtonDown(0))
            {
                projectileThruster.gameObject.GetComponent<ThrusterController>().LaunchProjectile();
                _playerRb.AddRelativeForce(Vector3.back * projectileRecoil, ForceMode.Impulse);
            }
        }

        void RecallProjectile()
        {
            if (Input.GetKey(KeyCode.R))
            {
                projectileThruster.gameObject.GetComponent<ThrusterController>().RecallProjectile();
                _focalPoint.GetComponent<FocalPointController>().SetFocalPoint(gameObject);
            }
        }
    }
}
