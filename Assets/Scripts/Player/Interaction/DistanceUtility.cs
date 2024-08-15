using UnityEngine;

namespace Player.Interaction
{
    public static class DistanceUtility
    {
        public static float CalculateDistance(Transform origin, Transform target)
        {
            return Vector3.Distance(origin.position, target.position);
        }

        public static bool IsWithinInteractionDistance(Transform origin, Transform target, float maxDistance)
        {
            float distance = CalculateDistance(origin, target);
            return distance <= maxDistance;
        }
    }
}
