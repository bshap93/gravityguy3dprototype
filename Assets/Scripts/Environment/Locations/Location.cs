using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

namespace Environment.Locations
{
    [System.Serializable]
    public class Location
    {
        public string id;
        [FormerlySerializedAs("name")] public string locationName;
        public string description;
        public Vector3 position;
        [FormerlySerializedAs("scene")] public string sceneName;
        [CanBeNull] public List<string> connectedLocations;
        [CanBeNull] public Dictionary<string, object> CustomProperties;

        public Location(string id, string locationName, string description, Vector3 position, string sceneName)
        {
            this.id = id;
            this.locationName = locationName;
            this.description = description;
            this.sceneName = sceneName;
            this.connectedLocations = null;
            CustomProperties = null;
        }
    }
}
