using System.Collections.Generic;
using UnityEngine;
using VSX.UniversalVehicleCombat.Radar;

namespace ShipControl.SCK_Specific
{
    public class SpaceShipTrackerController : MonoBehaviour
    {
        public List<Trackable> targets;
        [SerializeField] Tracker spaceShipTracker;
        // Start is called before the first frame update
        void Start()
        {
            if (targets.Count > 0)
            {
                foreach (Trackable target in targets)
                {
                    target.SetActivation(true);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
