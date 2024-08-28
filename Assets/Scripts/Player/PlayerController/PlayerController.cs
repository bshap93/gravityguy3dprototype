using System;
using System.Collections;
using Environment.Area;
using GameManager;
using JetBrains.Annotations;
using Player.Audio;
using Player.InGameResources;
using Player.PlayerController.Components;
using ShipControl;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;
using Sirenix.OdinInspector;

namespace Player.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        [SerializeField] private ShipMovement shipMovement;
        [SerializeField] private PlayerCameraController cameraController;
        [SerializeField] private AttachmentManager attachmentManager;
        [SerializeField] private FuelSystem fuelSystem;
        [SerializeField] private EngineAudioManager engineAudioManager;
        [SerializeField] private PlayerShipControlInput playerShipControlInput;
        [SerializeField] private AreaManager areaManager;
        [SerializeField] private float maxSpeed = 100f;

        private IShip currentShip;
        private VelocityTracker velocityTracker;
        private Vector3 initialPosition;


        public AreaGenerator areaGenerator;
        public Transform playerShip;
        public Transform attachmentPoint;
        public Attachment currentAttachment;
        GameObject _focalPoint;
        float _originalPlayerRotationVolume;
        float _originalPlayerThrusterVolume;
        [SerializeField] float thrustPowerInNewtons = 1000f; // 1 kN of thrust
        [SerializeField] float specificImpulseInSeconds = 1000000f; // Very high for fusion propulsion
        [SerializeField] float flipTorque = 10f;
        [SerializeField] float minVelocityForFlip = 5f;
        [SerializeField] float alignmentThreshold = 0.95f;
        Vector3 _flipTarget;
        [SerializeField] SpaceShipController spaceShipController;
        VelocityTracker _velocityTracker;
        Vector3 _initialPosition;


        private void Awake()
        {
            Instance = this;
            _initialPosition = transform.position;
        }

        void Start()
        {
            _velocityTracker = GetComponent<VelocityTracker>();
            InitializeShip();
            EventManager.Instance.deathEvent.AddListener(OnPlayerDeath);

            OdinDebugStartManager debugManager = FindObjectOfType<OdinDebugStartManager>();
            if (debugManager != null && debugManager.debugStartPoints.Count > 0)
            {
                // The game was started in debug mode, so don't do normal initialization
                return;
            }
        }

        private void InitializeShip()
        {
            currentShip = GetComponent<IShip>();
            if (currentShip == null)
            {
                Debug.LogError("No IShip component found on the player object!");
            }

            shipMovement.Initialize(GetComponent<Rigidbody>());
        }

        void Update()
        {
            cameraController?.LockRotation();
            UpdateEngineAudio();
        }

        void FixedUpdate()
        {
            HandleMovement();
            HandleWeaponFiring();
        }

        private void HandleMovement()
        {
            shipMovement.UpdateMovement(
                playerShipControlInput.VerticalInput,
                playerShipControlInput.HorizontalInput,
                playerShipControlInput.IsBraking,
                velocityTracker.GetLinearVelocity().magnitude
            );

            if (playerShipControlInput.IsBraking)
            {
                currentShip.HandleBrakingThrusters();
                currentShip.HandleBrakingAngularThrusters();
            }
        }

        private void HandleWeaponFiring()
        {
            currentShip.FireMainWeaponOnce(playerShipControlInput.FireInputDown);
            currentShip.FireMainWeaponContinuous(playerShipControlInput.FireInputSustained);
        }
        
        
        void UpdateEngineAudio()
        {
            float currentSpeed = shipMovement.playerRb.velocity.magnitude;
            bool isThrusting = playerShipControlInput.VerticalInput != 0;
            bool hasActiveInput = isThrusting || Input.GetKey(KeyCode.LeftShift);
            bool hasFuel = fuelSystem.HasFuel();

            engineAudioManager.UpdateEngineSounds(
                currentSpeed, maxSpeed, isThrusting, hasActiveInput, hasFuel);
        }
        
        public void RefuelShip(float amount)
        {
            fuelSystem.RefuelShip(amount);
        }
        
        void OnPlayerDeath(string arg0)
        {
            transform.position = _initialPosition;
        } 
        
        void OnCollisionEnter(Collision other)
        {
            Debug.Log("Collision detected");
            var linearVelocity = _velocityTracker.GetLinearVelocity().magnitude;
            engineAudioManager.PlayCollisionHit(linearVelocity);
        }
        
        /// <summary>
        /// Future implementation of switching ships
        /// and attaching upgrades and moving to new areas
        /// </summary>
        /// <param name="newShip"></param>
        
        public void SwitchShip(IShip newShip)
        {
            // Disable the current ship
            if (currentShip != null)
            {
                (currentShip as MonoBehaviour)?.gameObject.SetActive(false);
            }

            // Enable the new ship
            currentShip = newShip;
            (currentShip as MonoBehaviour)?.gameObject.SetActive(true);

            // Reinitialize components with the new ship
            InitializeShip();
        }
        



        public void TravelToNewArea()
        {
            StartCoroutine(TransitionToNewArea());
        }

        private IEnumerator TransitionToNewArea()
        {
            // Fade out
            yield return StartCoroutine(FadeEffect(1f));

            // Generate new area and move player
            Vector3 newAreaCenter = Random.insideUnitSphere * 10000f;
            areaGenerator.GenerateNewArea(newAreaCenter);
            playerShip.position = newAreaCenter;

            // Fade in
            yield return StartCoroutine(FadeEffect(0f));
        }

        private IEnumerator FadeEffect(float targetAlpha)
        {
            // Implement fade effect here

            yield return null;
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





    }
}
