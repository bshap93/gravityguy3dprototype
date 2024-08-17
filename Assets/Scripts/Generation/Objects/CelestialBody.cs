using UnityEngine;

namespace Generation.Objects
{
    [System.Serializable]
    public class CelestialBody
    {
        public string name;
        public CelestialBodyType type;
        public Vector3 position;
        public float size; // in Earth radii
        public float mass; // in Earth masses
        public float orbitalPeriod; // in Earth years
        public float orbitalRadius; // in AU
        public Color color;
    }
}
