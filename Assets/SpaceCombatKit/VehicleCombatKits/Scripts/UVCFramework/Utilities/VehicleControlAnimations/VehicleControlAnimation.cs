using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VSX.UniversalVehicleCombat
{
    public class VehicleControlAnimation : MonoBehaviour
    {
        public virtual Quaternion GetRotation()
        {
            return Quaternion.Euler(0, 0, 0);
        }

        public virtual Vector3 GetPosition()
        {
            return Vector3.zero;
        }
    }
}

