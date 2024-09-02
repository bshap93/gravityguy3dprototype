using Cinemachine;
using UnityEngine;

namespace CameraScripts
{
    public class CameraZoom : MonoBehaviour
    {
        public CinemachineFreeLook cinemachineFreeLook;
        public float zoomSpeed = 10f; // Speed of the zoom
        public float minZoom = 5f; // Minimum zoom distance
        public float maxZoom = 20f; // Maximum zoom distance

        void Update()
        {
            if (cinemachineFreeLook != null)
            {
                // Get mouse wheel input
                float mouseWheel = Input.GetAxis("Mouse ScrollWheel");

                // Calculate new radius
                float newRadius = cinemachineFreeLook.m_Orbits[1].m_Radius - mouseWheel * zoomSpeed;
                newRadius = Mathf.Clamp(newRadius, minZoom, maxZoom);

                // Set the radius for all orbits (you can adjust this to only affect certain orbits)
                for (int i = 0; i < cinemachineFreeLook.m_Orbits.Length; i++)
                {
                    cinemachineFreeLook.m_Orbits[i].m_Radius = newRadius;
                }
            }
        }
    }
}
