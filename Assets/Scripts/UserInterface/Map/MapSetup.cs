using UnityEngine;

namespace UserInterface.Map
{
    public class MapSetup : MonoBehaviour
    {
        public MapLocationManager locationManager;

        void Start()
        {
            // Add locations (x and y should be between 0 and 1)
            locationManager.AddLocationName("Hab Skiff", new Vector2(0.4f, 0.3f));
            locationManager.AddLocationName("Fleet Headquarters", new Vector2(0.6f, 0.8f));
            locationManager.AddLocationName("Separatists", new Vector2(0.8f, 0.4f));
        }
    }
}
