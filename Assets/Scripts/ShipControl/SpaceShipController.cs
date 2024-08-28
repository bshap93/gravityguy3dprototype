using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Player.Effects;
using Player.PlayerController.Components;
using ShipControl.SCK_Specific;
using UnityEngine;
using UnityEngine.Serialization;
using VSX.UniversalVehicleCombat;

namespace ShipControl
{
    public class SpaceShipController : MonoBehaviour, IShip
    {
        [SerializeField] private GameObject shapeVisual;
        [SerializeField] public GameObject mainFusionThruster;
        [SerializeField] private GameObject collidersObject;
        [SerializeField] private GameObject attitudeThrusterPrefab;
        [SerializeField] private AttitudeJetsController attitudeJetsController;
        [SerializeField] private VelocityTracker velocityTracker;
        [SerializeField] private GameObject gunsPrefab;
        [SerializeField] private ShipMainWeapon shipMainWeapon;

        // [SerializeField] List<MeshCollider> meshColliderComponents;

        public float LinearThreshold { get; set; } = 0.1f;
        public float AngularThreshold { get; set; } = 0.1f;

        void Start()
        {
            InitializeShipComponents();
        }

        private void InitializeShipComponents()
        {
            GetShipBaseComponents();
            InitiateAttitudeThrusters();
            InitializeWeapons();
        }

        private void GetShipBaseComponents()
        {
            shapeVisual = transform.Find("Shape Visual")?.gameObject;
            mainFusionThruster = transform.Find("Thruster(Clone)")?.gameObject;
            collidersObject = transform.Find("Colliders")?.gameObject;
        }


        private void InitiateAttitudeThrusters()
        {
            var attitudeThruster = Instantiate(attitudeThrusterPrefab, transform);
            attitudeThruster.transform.localPosition = Vector3.zero;
            attitudeThruster.transform.localRotation = Quaternion.identity;
            attitudeThruster.transform.localScale = Vector3.one;
            attitudeJetsController = attitudeThruster.GetComponent<AttitudeJetsController>();
        }

        private void InitializeWeapons()
        {
            var guns = Instantiate(gunsPrefab, transform);
            guns.transform.localPosition = Vector3.zero;
            guns.transform.localRotation = Quaternion.identity;
            guns.transform.localScale = Vector3.one;
            shipMainWeapon = guns.GetComponent<ShipMainWeapon>();
            if (shipMainWeapon != null) shipMainWeapon.spaceShipController = this;
        }


        public void HandleBrakingThrusters()
        {
            var linearVelocity = velocityTracker.GetLinearVelocity();


            if (linearVelocity.magnitude > LinearThreshold)
            {
                Vector2 oppositeForce = -linearVelocity.normalized;

                if (Vector2.Dot(oppositeForce, Vector2.right) > 0)
                {
                    ThrustForward();
                }
                else
                {
                    ThrustBackward();
                }
            }
            else
            {
                EndThrusterForward();
                EndThrusterBackward();
            }
        }

        public void HandleBrakingAngularThrusters()
        {
            var angularVelocity = velocityTracker.GetAngularVelocity();
            if (Mathf.Abs(angularVelocity) > AngularThreshold)
            {
                if (angularVelocity > 0)
                {
                    ThrustLeft();
                }
                else
                {
                    ThrustRight();
                }
            }
            else if (Mathf.Abs(angularVelocity) < AngularThreshold)
            {
                EndThrusterLeft();
                EndThrusterRight();
            }
            else
            {
                EndThrusterLeft();
                EndThrusterRight();
            }
        }

        public void ThrustForward() => attitudeJetsController.ThrustForward();
        public void EndThrusterForward() => attitudeJetsController.EndThrusterForward();
        public void ThrustBackward() => attitudeJetsController.ThrustBackward();
        public void EndThrusterBackward() => attitudeJetsController.EndThrusterBackward();
        public void ThrustLeft() => attitudeJetsController.ThrustLeft();
        public void EndThrusterLeft() => attitudeJetsController.EndThrusterLeft();
        public void ThrustRight() => attitudeJetsController.ThrustRight();

        public void EndThrusterRight() => attitudeJetsController.EndThrusterRight();
        public void FireMainWeaponOnce(bool isFiring) => shipMainWeapon.FireWeapon(isFiring);
        public void FireMainWeaponContinuous(bool isFiring) => shipMainWeapon.FireWeaponContinuous(isFiring);

        // Not sure if needed
        public void EndThrustForward()
        {
            throw new NotImplementedException();
        }
        public void EndThrustBackward()
        {
            throw new NotImplementedException();
        }
        public void EndThrustLeft()
        {
            throw new NotImplementedException();
        }
        public void EndThrustRight()
        {
            throw new NotImplementedException();
        }
    }
}
