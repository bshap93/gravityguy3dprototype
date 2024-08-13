using System.Collections.Generic;
using UnityEngine;

namespace Environment.Locations
{
    [CreateAssetMenu(fileName = "A New Location", menuName = "Game/Location")]
    public class LocationScriptableObject : ScriptableObject
    {
        public string id;
        public string locationName;
        [TextArea(3, 10)] public string description;
        public Vector3 position;

        public string sceneName;
        public List<string> connectedLocationIds;
        public Dictionary<string, object> CustomProperties;

        public Location ToLocation()
        {
            Location location = new Location(id, locationName, description, position, sceneName);


            return location;
        }
    }
}
