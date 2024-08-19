using UnityEngine;

namespace Effects
{
    public enum ThrusterRole
    {
        Projectile,
        RearThruster,
        AttitudeJet
    }

    public class ThrusterController : MonoBehaviour
    {
        public bool isThrusting;
    }
}
