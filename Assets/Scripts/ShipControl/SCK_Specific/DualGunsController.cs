using UnityEngine;
using VSX.UniversalVehicleCombat;

namespace ShipControl.SCK_Specific
{
    public class DualGunsController : MonoBehaviour
    {
        [SerializeField] Triggerable dualGuneTriggerable;
        // Start is called before the first frame update
        void Start()
        {
            dualGuneTriggerable = GetComponent<Triggerable>();
        }

        public void Fire()
        {
            dualGuneTriggerable.StartTriggering();
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
