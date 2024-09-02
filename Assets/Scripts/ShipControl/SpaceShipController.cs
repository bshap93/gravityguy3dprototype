using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Player.Effects;
using Player.PlayerController.Components;
using ShipControl.Movement;
using ShipControl.SCK_Specific;
using UnityEngine;
using UnityEngine.Serialization;
using VSX.UniversalVehicleCombat;

namespace ShipControl
{
    public class SpaceShipController : MonoBehaviour
    {
        [FormerlySerializedAs("thruster")] [SerializeField]
        public GameObject mainFusionThruster;

        [SerializeField] public GameObject attitudeThrusterPrefab;
        [SerializeField] public AttitudeJetsController attitudeJetsController;
        [SerializeField] public MainTorchController mainTorchController;
        [SerializeField] public VelocityTracker velocityTracker;
        [SerializeField] public GameObject gunsPrefab;
        [SerializeField] public ShipMainWeapon shipMainWeapon;


        public float linearThreshold = 0.1f; // Minimum linear velocity to activate thrusters
        public float angularThreshold = 0.1f; // Minimum angular velocity to activate thrusters


        void Start()
        {
            // Ship Control Components
            InitiateAttitudeThrusters();
            var guns = Instantiate(gunsPrefab, transform, true);
            guns.transform.localPosition = new Vector3(0, 0, 0);
            guns.transform.localRotation = new Quaternion(0, 0, 0, 1);
            guns.transform.localScale = new Vector3(1, 1, 1);
            shipMainWeapon = guns.GetComponent<ShipMainWeapon>();
            if (shipMainWeapon != null) shipMainWeapon.spaceShipController = this;
        }
        void InitiateAttitudeThrusters()
        {
            var attitudeThruster = Instantiate(attitudeThrusterPrefab, transform, true);
            attitudeThruster.transform.localPosition = new Vector3(0, 0, 0);
            attitudeThruster.transform.localRotation = new Quaternion(0, 0, 0, 1);
            attitudeThruster.transform.localScale = new Vector3(1, 1, 1);
            attitudeJetsController = attitudeThruster.GetComponent<AttitudeJetsController>();
        }


        // ReSharper disable Unity.PerformanceAnalysis
        public void HandleBrakingThrusters()
        {
            var linearVelocity = velocityTracker.GetLinearVelocity();


            if (linearVelocity.magnitude > linearThreshold)
            {
                Vector2 oppositeForce = -linearVelocity.normalized;

                if (Vector2.Dot(oppositeForce, Vector2.right) > 0)
                {
                    ThrustForward(ThrustType.AttitudeJet);
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
            if (Mathf.Abs(angularVelocity) > angularThreshold)
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
            else if (Mathf.Abs(angularVelocity) < angularThreshold)
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

        public void ThrustForward(ThrustType thrustType)
        {
            if (thrustType == ThrustType.Torch)
            {
                mainTorchController.SetTorchState(TorchState.Full);
            }
            else
            {
                attitudeJetsController.AttitudeThrustForward();
            }
        }

        public void EndThrusterForward()
        {
            attitudeJetsController.EndThrusterForward();
        }

        public void ThrustBackward()
        {
            attitudeJetsController.ThrustBackward();
        }

        public void EndThrusterBackward()
        {
            attitudeJetsController.EndThrusterBackward();
        }

        public void ThrustLeft()
        {
            attitudeJetsController.ThrustLeft();
        }

        public void EndThrusterLeft()
        {
            attitudeJetsController.EndThrusterLeft();
        }

        public void ThrustRight()
        {
            attitudeJetsController.ThrustRight();
        }

        public void EndThrusterRight()
        {
            attitudeJetsController.EndThrusterRight();
        }

        public void FireMainWeaponOnce(bool isFiring)
        {
            shipMainWeapon.FireWeapon(isFiring);
        }

        public void FireMainWeaponContinuous(bool isFiring)
        {
            shipMainWeapon.FireWeaponContinuous(isFiring);
        }
    }
}
