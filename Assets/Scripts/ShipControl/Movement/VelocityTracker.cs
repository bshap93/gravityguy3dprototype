using TMPro;
using UnityEngine;

namespace ShipControl
{
    public class VelocityTracker : MonoBehaviour
    {
        public TMP_Text velocityText; // UI Text to display linear velocity
        public TMP_Text angularVelocityText; // UI Text to display angular velocity
        public float updateInterval = 0.1f; // How often to update the velocity

        private Vector3 velocity;
        private float angularVelocity;
        private float timer;

        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();

            if (rb == null)
            {
                Debug.LogError("VelocityTracker requires a Rigidbody component on the same GameObject.");
            }
        }

        void Update()
        {
            timer += Time.deltaTime;

            if (timer >= updateInterval)
            {
                // Calculate linear velocity using Rigidbody
                velocity = rb.velocity;
                velocity.y = 0; // Lock Y axis for linear velocity

                // Calculate angular velocity (rotation around Y axis)
                angularVelocity = rb.angularVelocity.y;

                // Update UI
                UpdateVelocityDisplay();

                timer = 0f;
            }
        }

        void UpdateVelocityDisplay()
        {
            if (velocityText != null)
            {
                float speed = new Vector2(velocity.x, velocity.z).magnitude;
                velocityText.text = $"Speed: {speed:F2} m/s\nVx: {velocity.x:F2}\nVz: {velocity.z:F2}";
            }

            if (angularVelocityText != null)
            {
                float angularSpeedDegrees = Mathf.Abs(angularVelocity) * Mathf.Rad2Deg;
                string rotationDirection = angularVelocity > 0 ? "Right" : "Left";
                angularVelocityText.text = $"Rotation: {angularSpeedDegrees:F2}°/s\nDirection: {rotationDirection}";
            }
        }

        public Vector2 GetLinearVelocity()
        {
            return new Vector2(velocity.x, velocity.z);
        }

        public float GetAngularVelocity()
        {
            return angularVelocity;
        }
    }
}
