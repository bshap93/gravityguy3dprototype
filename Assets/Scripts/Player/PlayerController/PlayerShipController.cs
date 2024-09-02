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
    public class PlayerShipController : MonoBehaviour
    {
        public static PlayerShipController Instance { get; private set; }

        [SerializeField] ShipMovement shipMovement;

        public AreaGenerator areaGenerator;
        public Transform playerShip;

        public void TravelToNewArea()
        {
            // Play transition effect
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


        [SerializeField] [CanBeNull] PlayerCameraController cameraController;
        [SerializeField] AttachmentManager attachmentManager;
        [SerializeField] FuelSystem fuelSystem;
        [SerializeField] EngineAudioManager engineAudioManager;
        [FormerlySerializedAs("inputManager")] [SerializeField]
        PlayerShipControlInput playerShipControlInput;

        [SerializeField] private AreaManager areaManager;


        [SerializeField] float maxSpeed = 100f; // Adjust based on your game's scale

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
            // Initialize Instance
            Instance = this;
            // Initialize components
            shipMovement.Initialize(GetComponent<Rigidbody>());
            _initialPosition = transform.position;
            // Subscribe to EventManager DetahEvent
        }
        void OnPlayerDeath(string arg0)
        {
            transform.position = _initialPosition;
        }
        void Start()
        {
            _velocityTracker = GetComponent<VelocityTracker>();

            EventManager.Instance.deathEvent.AddListener(OnPlayerDeath);

            OdinDebugStartManager debugManager = FindObjectOfType<OdinDebugStartManager>();
            if (debugManager != null && debugManager.debugStartPoints.Count > 0)
            {
                // The game was started in debug mode, so don't do normal initialization
                return;
            }
        }
        // Update is called once per frame
        void Update()
        {
            cameraController?.LockRotation(!Input.GetMouseButton(2));
            UpdateEngineAudio();
        }

        void FixedUpdate()
        {
            shipMovement.UpdateMovement(
                playerShipControlInput.VerticalInput,
                playerShipControlInput.HorizontalInput,
                playerShipControlInput.IsBraking,
                _velocityTracker.GetLinearVelocity().magnitude);


            spaceShipController.FireMainWeaponOnce(playerShipControlInput.FireInputDown);
            spaceShipController.FireMainWeaponContinuous(playerShipControlInput.FireInputSustained);
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

        void OnCollisionEnter(Collision other)
        {
            Debug.Log("Collision detected");
            var linearVelocity = _velocityTracker.GetLinearVelocity().magnitude;
            engineAudioManager.PlayCollisionHit(linearVelocity);
        }
    }
}
