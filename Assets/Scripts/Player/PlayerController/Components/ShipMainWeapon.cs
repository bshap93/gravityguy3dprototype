using JetBrains.Annotations;
using ShipControl;
using ShipControl.SCK_Specific;
using UnityEngine;
using VSX.UniversalVehicleCombat;

namespace Player.PlayerController.Components
{
    public class ShipMainWeapon : MonoBehaviour
    {
        [SerializeField] public SpaceShipController spaceShipController;
        [SerializeField] Weapon mainWeapon;
        [SerializeField] Module mainWeaponModule;
        [SerializeField] Triggerable mainWeaponTriggerable;

        public void Start()
        {
            mainWeapon = GetComponent<Weapon>();
            mainWeaponModule = GetComponent<Module>();
            mainWeaponTriggerable = GetComponent<Triggerable>();
        }

        // Fire weapon
        public void FireWeapon(bool isFiring)
        {
            if (isFiring)
            {
                mainWeaponTriggerable.TriggerOnce();
            }
            // else
            // {
            //     mainWeaponTriggerable.StopTriggering();
            // }
        }

        public void FireWeaponContinuous(bool isFiring)
        {
            if (!mainWeaponTriggerable) return;
            if (isFiring)
            {
                mainWeaponTriggerable.StartTriggering();
            }
            else if (mainWeaponTriggerable.Triggering)
            {
                mainWeaponTriggerable.StopTriggering();
            }
        }
    }
}
