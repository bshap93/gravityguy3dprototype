using UnityEngine;

namespace CameraScripts.Minimap
{
    public class PointOfInterest : MonoBehaviour
    {
        public enum PoiType
        {
            Player,
            Asteroid,
            Planet,
            Station,
            Objective,
            Ship,
            // Add more types as needed
        }

        public PoiType type;
        public Sprite minimapIcon;
        public string poiName;
    }
}
