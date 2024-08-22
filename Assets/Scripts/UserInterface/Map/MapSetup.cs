using UnityEngine;

namespace UserInterface.Map
{
    public class MapSetup : MonoBehaviour
    {
        MapLocationManager _locationManager;

        void Start()
        {
            _locationManager = MapLocationManager.Instance;
            // Add locations (x and y should be between 0 and 1)
            _locationManager.AddLocationName("Starship Enoch", new Vector2(0.5f, 0.5f));
            _locationManager.AddLocationName("", new Vector2(0.6f, 0.8f));
            _locationManager.AddLocationName("Freehold Station", new Vector2(0.8f, 0.6f));
        }
    }
}
