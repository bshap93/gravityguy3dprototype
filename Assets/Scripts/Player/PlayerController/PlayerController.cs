using System;
using System.Collections;
using JetBrains.Annotations;
using Player.Audio;
using Player.PlayerController.Components;
using Player.Resources;
using ShipControl;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.PlayerController
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance { get; private set; }

        [SerializeField] ShipMovement shipMovement;

        [SerializeField] [CanBeNull] PlayerCameraController cameraController;
        [SerializeField] AttachmentManager attachmentManager;
        [SerializeField] FuelSystem fuelSystem;
        [SerializeField] EngineAudioManager engineAudioManager;
        [SerializeField] InputManager inputManager;

        [SerializeField] float maxSpeed = 100f; // Adjust based on your game's scale

        public Transform attachmentPoint;

        public Attachment currentAttachment;


        GameObject _focalPoint;


        float _originalPlayerRotationVolume;

        float _originalPlayerThrusterVolume;


        Rigidbody _playerRb;


        [SerializeField] float thrustPowerInNewtons = 1000f; // 1 kN of thrust
        [SerializeField] float specificImpulseInSeconds = 1000000f; // Very high for fusion propulsion

        [SerializeField] float flipTorque = 10f;
        [SerializeField] float minVelocityForFlip = 5f;
        [SerializeField] float alignmentThreshold = 0.95f;
        Vector3 _flipTarget;

        [SerializeField] SpaceShipController spaceShipController;


        private void Awake()
        {
            // Initialize Instance
            Instance = this;
            // Initialize components
            shipMovement.Initialize(GetComponent<Rigidbody>());
        }
        void Start()
        {
            _playerRb = GetComponent<Rigidbody>();
        }
        // Update is called once per frame
        void Update()
        {
            cameraController?.LockRotation();
            UpdateEngineAudio();
        }

        void FixedUpdate()
        {
            shipMovement.ApplyThrust(inputManager.VerticalInput);
            shipMovement.ApplyRotationalThrust(inputManager.HorizontalInput);
            shipMovement.ApplyBraking();
            spaceShipController.FireMainWeaponOnce(inputManager.FireInputDown);
            spaceShipController.FireMainWeaponContinuous(inputManager.FireInputSustained);
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
            float currentSpeed = _playerRb.velocity.magnitude;
            bool isThrusting = inputManager.VerticalInput != 0;
            bool hasActiveInput = isThrusting || Input.GetKey(KeyCode.LeftShift);
            bool hasFuel = fuelSystem.HasFuel();

            engineAudioManager.UpdateEngineSounds(currentSpeed, maxSpeed, isThrusting, hasActiveInput, hasFuel);
        }


        public void RefuelShip(float amount)
        {
            fuelSystem.RefuelShip(amount);
        }

        void OnCollisionEnter(Collision other)
        {
            Debug.Log("Collision detected");
            var velocityTracker = GetComponent<VelocityTracker>();
            var linearVelocity = velocityTracker.GetLinearVelocity().magnitude;
            engineAudioManager.PlayCollisionHit(linearVelocity);
        }
    }
}
