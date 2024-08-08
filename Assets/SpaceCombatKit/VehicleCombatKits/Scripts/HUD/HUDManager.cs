using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSX.UniversalVehicleCombat.Radar
{
    /// <summary>
    /// Manages all the components of the HUD.
    /// </summary>
    [DefaultExecutionOrder(30)]
    public class HUDManager : MonoBehaviour, ICamerasUser
    {
        [Tooltip("The index of the HUD camera within the list of cameras (passed by the Camera Entity).")]
        [SerializeField]
        protected int hudCameraIndex = 0;
        
        protected List<HUDComponent> hudComponents = new List<HUDComponent>();

        protected bool activated = false;
        public virtual bool Activated { get { return activated; } }

        [Tooltip("Whether to activate the HUD when the scene starts.")]
        [SerializeField]
        protected bool activateOnStart = false;

        protected IHUDCameraUser[] m_HUDCameraUsers;


        protected virtual void Awake()
        {

            hudComponents = new List<HUDComponent>(transform.GetComponentsInChildren<HUDComponent>());
            m_HUDCameraUsers = transform.GetComponentsInChildren<IHUDCameraUser>();

            Vehicle vehicle = transform.GetComponent<Vehicle>();
            if (vehicle != null)
            {
                vehicle.onDestroyed.AddListener(DeactivateHUD);
            }
        }
        

        /// <summary>
        /// Pass the list of cameras to the HUD.
        /// </summary>
        /// <param name="cameras">The list of cameras.</param>
        public virtual void SetCameras(List<Camera> cameras)
        {
            if (cameras.Count > hudCameraIndex)
            {
                for (int i = 0; i < m_HUDCameraUsers.Length; ++i)
                {
                    m_HUDCameraUsers[i].HUDCamera = cameras[hudCameraIndex];
                }
            }
        }


        // Called when the scene starts
        protected virtual void Start()
        {
            if (!activated)
            {
                if (activateOnStart)
                {
                    ActivateHUD();
                }
                else
                {
                    DeactivateHUD();
                }
            }
        }


        /// <summary>
        /// Set the camera for the HUD.
        /// </summary>
        /// <param name="hudCamera">The HUD camera.</param>
        public virtual void SetHUDCamera(Camera hudCamera)
        {
            for (int i = 0; i < m_HUDCameraUsers.Length; ++i)
            {
                m_HUDCameraUsers[i].HUDCamera = hudCamera;
            }
        }


        /// <summary>
        /// Activate the HUD.
        /// </summary>
        public virtual void ActivateHUD()
        {
            for (int i = 0; i < hudComponents.Count; ++i)
            {
                if (hudComponents[i] != null)
                {
                    hudComponents[i].Activate();
                }
            }

            activated = true;
        }


        /// <summary>
        /// Deactivate the HUD.
        /// </summary>
        public virtual void DeactivateHUD()
        {
            for (int i = 0; i < hudComponents.Count; ++i)
            {
                if (hudComponents[i] != null)   // Necessary because when OnDisable is called when scene is being destroyed, not checking causes error
                {
                    hudComponents[i].Deactivate();
                }
            }

            activated = false;
        }


        // Called every frame
        public virtual void LateUpdate()
        {
            if (activated)
            {
                for (int i = 0; i < hudComponents.Count; ++i)
                {
                    if (hudComponents[i].Activated)
                    {
                        hudComponents[i].OnUpdateHUD();
                    }
                }
            }
        }
    }
}
