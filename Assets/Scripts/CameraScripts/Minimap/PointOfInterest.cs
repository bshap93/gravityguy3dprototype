using UnityEngine;

namespace CameraScripts.Minimap
{
    public class PointOfInterest : MonoBehaviour
    {
        public enum POIType
        {
            Player,
            Asteroid,
            Planet,
            Station,
            Objective,
            Ship,
            // Add more types as needed
        }

        public POIType type;
        public Sprite minimapIcon;
        public string poiName;
    }
}
