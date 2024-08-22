﻿using UnityEngine;

namespace Player.PlayerController.Components
{
    public class InputManager : MonoBehaviour
    {
        public float VerticalInput { get; private set; }
        public float HorizontalInput { get; private set; }
        public bool IsBraking { get; private set; }
        public bool FireInputSustained { get; set; }

        public bool FireInputDown { get; set; }
        public bool FireInputUp { get; set; }

        private void Update()
        {
            VerticalInput = Input.GetAxis("Vertical");
            HorizontalInput = Input.GetAxis("Horizontal");
            IsBraking = Input.GetKey(KeyCode.Space);
            GetWeaponsFiringInputs();
        }
        void GetWeaponsFiringInputs(int mouseButton = 1)
        {
            FireInputSustained = Input.GetMouseButton(mouseButton);
            FireInputDown = Input.GetMouseButtonDown(mouseButton);
            FireInputUp = Input.GetMouseButtonUp(mouseButton);
        }
    }
}