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
    public class SpaceShipController : MonoBehaviour
    {
        [SerializeField] public GameObject shapeVisual;
        [FormerlySerializedAs("thruster")] [SerializeField]
        public GameObject mainFusionThruster;
        [FormerlySerializedAs("colliders")] [SerializeField]
        public GameObject collidersObject;
        [SerializeField] public GameObject attitudeThrusterPrefab;
        [SerializeField] public AttitudeJetsController attitudeJetsController;
        [SerializeField] public VelocityTracker velocityTracker;
        [SerializeField] public GameObject gunsPrefab;
        [SerializeField] public ShipMainWeapon shipMainWeapon;

        // [SerializeField] List<MeshCollider> meshColliderComponents;

        public float linearThreshold = 0.1f; // Minimum linear velocity to activate thrusters
        public float angularThreshold = 0.1f; // Minimum angular velocity to activate thrusters


        void Start()
        {
            // Ship Base Components
            GetShipBaseComponents();
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
        void GetShipBaseComponents()
        {
            shapeVisual = GameObject.Find("Shape Visual");
            GameObject.Find("Decal Visual");
            mainFusionThruster = GameObject.Find("Thruster(Clone)");
            collidersObject = GameObject.Find("Colliders");
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

        public void ThrustForward()
        {
            Debug.Log("Thrust Forward");
            attitudeJetsController.ThrustForward();
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

        void OnCollisionEnter(Collision other)
        {
            Debug.Log("Collision detected");
        }

        void OnCollisionExit(Collision other)
        {
            Debug.Log("Collision Exit");
        }

        void OnCollisionStay(Collision other)
        {
            Debug.Log("Collision Stay");
        }
    }
}
