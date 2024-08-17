using System.Collections.Generic;

namespace Generation.Objects
{
    [System.Serializable]
    public class StarSystem
    {
        public string name;
        public Star star;
        public List<CelestialBody> celestialBodies;
        public List<Settlement> settlements;

        public StarSystem(string name)
        {
            this.name = name;
            celestialBodies = new List<CelestialBody>();
            settlements = new List<Settlement>();
        }
    }
}
