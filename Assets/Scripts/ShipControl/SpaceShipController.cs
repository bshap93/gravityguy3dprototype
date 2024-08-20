using Player.Effects;
using UnityEngine;
using UnityEngine.Serialization;

namespace ShipControl
{
    public class SpaceShipController : MonoBehaviour
    {
        [SerializeField] public GameObject shapeVisual;
        [SerializeField] public GameObject decalVisual;
        [FormerlySerializedAs("thruster")] [SerializeField]
        public GameObject mainFusionThruster;
        [SerializeField] public GameObject colliders;
        [SerializeField] public GameObject attitudeThrusterPrefab;
        [SerializeField] public AttitudeJetsController attitudeJetsController;
        [SerializeField] public VelocityTracker velocityTracker;

        public float linearThreshold = 0.1f; // Minimum linear velocity to activate thrusters
        public float angularThreshold = 0.1f; // Minimum angular velocity to activate thrusters


        void Start()
        {
            shapeVisual = GameObject.Find("Shape Visual");
            decalVisual = GameObject.Find("Decal Visual");
            mainFusionThruster = GameObject.Find("Thruster(Clone)");
            colliders = GameObject.Find("Colliders");
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
    }
}
