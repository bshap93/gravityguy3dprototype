using Cinemachine;
using UnityEngine;

namespace Player.PlayerController.Components
{
    public class PlayerCameraController : MonoBehaviour
    {
        public CinemachineFreeLook playerFreeLookCamera;
        float _originalFreeLookXMaxSpeed;
        float _originalFreeLookYMaxSpeed;

        private void Start()
        {
            _originalFreeLookXMaxSpeed = playerFreeLookCamera.m_XAxis.m_MaxSpeed;
            _originalFreeLookYMaxSpeed = playerFreeLookCamera.m_YAxis.m_MaxSpeed;
        }

        public void LockRotation()
        {
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                playerFreeLookCamera.m_YAxis.m_MaxSpeed = 0;
                playerFreeLookCamera.m_XAxis.m_MaxSpeed = 0;
            }
            else
            {
                playerFreeLookCamera.m_YAxis.m_MaxSpeed = _originalFreeLookYMaxSpeed;
                playerFreeLookCamera.m_XAxis.m_MaxSpeed = _originalFreeLookXMaxSpeed;
            }
        }
        public void UnlockRotation()
        {
        }
    }
}
