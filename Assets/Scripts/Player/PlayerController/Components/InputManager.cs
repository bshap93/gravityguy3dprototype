using UnityEngine;

namespace Player.PlayerController.Components
{
    public class InputManager : MonoBehaviour
    {
        public float VerticalInput { get; private set; }
        public float HorizontalInput { get; private set; }
        public bool IsBraking { get; private set; }

        private void Update()
        {
            VerticalInput = Input.GetAxis("Vertical");
            HorizontalInput = Input.GetAxis("Horizontal");
            IsBraking = Input.GetKey(KeyCode.Space);
        }

        public void SetVerticalInput(float input)
        {
            VerticalInput = input;
        }

        public void SetHorizontalInput(float input)
        {
            HorizontalInput = input;
        }
    }
}
