using Generation.Objects;
using UnityEngine;

namespace Generation.Generators
{
    public class SystemGenerator : MonoBehaviour
    {
        private System.Random random;

        public StarSystem GenerateTauCetiSystem()
        {
            random = new System.Random(System.DateTime.Now.Millisecond); // Seed for procedural generation
            StarSystem system = new StarSystem("Tau Ceti");

            // Create Tau Ceti star
            system.star = new Star
            {
                name = "Tau Ceti",
                size = 0.793f, // In solar radii
                mass = 0.783f, // In solar masses
                color = new Color(1f, 0.9f, 0.7f), // G-type star, slightly orange
                position = Vector3.zero
            };

            // Known exoplanets (as of 2021)
            AddPlanet(system, "Tau Ceti g", 1.75f, 0.133f);
            AddPlanet(system, "Tau Ceti h", 1.83f, 0.243f);
            AddPlanet(system, "Tau Ceti e", 3.93f, 0.538f);
            AddPlanet(system, "Tau Ceti f", 3.93f, 1.334f);

            // Add some procedurally generated bodies
            AddAsteroidBelt(system, 2.5f, 3.5f);
            AddRandomPlanets(system, 2);
            AddRandomDwarfPlanets(system, 3);

            return system;
        }

        private void AddPlanet(StarSystem system, string name, float mass, float orbitalRadius)
        {
            system.celestialBodies.Add(
                new CelestialBody
                {
                    name = name,
                    type = CelestialBodyType.TerrestrialPlanet,
                    mass = mass,
                    size = Mathf.Pow(mass, 0.3f), // Rough estimate of radius based on mass
                    orbitalRadius = orbitalRadius,
                    orbitalPeriod = Mathf.Pow(orbitalRadius, 1.5f), // Kepler's Third Law
                    position = RandomOrbitPosition(orbitalRadius),
                    color = RandomPlanetColor()
                });
        }

        private void AddAsteroidBelt(StarSystem system, float innerRadius, float outerRadius)
        {
            system.celestialBodies.Add(
                new CelestialBody
                {
                    name = "Tau Ceti Asteroid Belt",
                    type = CelestialBodyType.AsteroidBelt,
                    orbitalRadius = (innerRadius + outerRadius) / 2f,
                    size = outerRadius - innerRadius,
                    position = Vector3.zero, // Belt circles the star
                    color = Color.gray
                });
        }

        private void AddRandomPlanets(StarSystem system, int count)
        {
            for (int i = 0; i < count; i++)
            {
                float orbitalRadius = RandomFloat(1.5f, 5f);
                float mass = RandomFloat(0.1f, 2f);
                AddPlanet(system, $"Tau Ceti {(char)('i' + i)}", mass, orbitalRadius);
            }
        }

        private void AddRandomDwarfPlanets(StarSystem system, int count)
        {
            for (int i = 0; i < count; i++)
            {
                system.celestialBodies.Add(
                    new CelestialBody
                    {
                        name = $"Tau Ceti Dwarf {i + 1}",
                        type = CelestialBodyType.DwarfPlanet,
                        mass = RandomFloat(0.01f, 0.1f),
                        size = RandomFloat(0.03f, 0.3f),
                        orbitalRadius = RandomFloat(4f, 8f),
                        orbitalPeriod = RandomFloat(8f, 20f),
                        position = RandomOrbitPosition(RandomFloat(4f, 8f)),
                        color = RandomPlanetColor()
                    });
            }
        }

        private Vector3 RandomOrbitPosition(float radius)
        {
            float angle = RandomFloat(0, 2 * Mathf.PI);
            return new Vector3(radius * Mathf.Cos(angle), 0, radius * Mathf.Sin(angle));
        }

        private Color RandomPlanetColor()
        {
            return new Color(RandomFloat(0.4f, 0.8f), RandomFloat(0.4f, 0.8f), RandomFloat(0.4f, 0.8f));
        }

        private float RandomFloat(float min, float max)
        {
            return (float)(random.NextDouble() * (max - min) + min);
        }
    }
}
