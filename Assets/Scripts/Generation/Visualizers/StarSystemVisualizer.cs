using UnityEngine;
using System.Collections.Generic;
using Generation.Generators;
using Generation.Objects;
using SpaceGraphicsToolkit;
using SpaceGraphicsToolkit.Atmosphere;
using SpaceGraphicsToolkit.Belt;
using SpaceGraphicsToolkit.Starfield;

public class SystemVisualizer : MonoBehaviour
{
    public SgtFloatingPoint floatingPoint;
    public Camera mainCamera;

    public SgtStarfield starfield;
    public SgtAtmosphere atmospherePrefab;
    public SgtPlanet planetPrefab;
    public SgtBelt asteroidBeltPrefab;

    public GameObject syntySpaceshipPrefab;
    public GameObject syntySpaceStationPrefab;

    public float scaleFactor = 1f; // Adjusted scale factor
    public float orbitScaleFactor = 1000f; // Scale factor for orbital distances

    private SystemGenerator generator;

    void Start()
    {
        if (!ValidatePrefabs())
        {
            Debug.LogError("Missing prefabs. Please assign all prefabs in the inspector.");
            return;
        }

        generator = GetComponent<SystemGenerator>();
        StarSystem system = generator.GenerateTauCetiSystem();
        SetupFloatingPointSystem();
        VisualizeSystem(system);
        SetupStarfield();
        PlacePlayerShip();
    }

    bool ValidatePrefabs()
    {
        if (atmospherePrefab == null)
        {
            Debug.LogError("Atmosphere prefab is not assigned!");
            return false;
        }

        if (planetPrefab == null)
        {
            Debug.LogError("Planet prefab is not assigned!");
            return false;
        }

        if (asteroidBeltPrefab == null)
        {
            Debug.LogError("Asteroid belt prefab is not assigned!");
            return false;
        }

        if (syntySpaceshipPrefab == null)
        {
            Debug.LogError("Spaceship prefab is not assigned!");
            return false;
        }

        if (syntySpaceStationPrefab == null)
        {
            Debug.LogError("Space station prefab is not assigned!");
            return false;
        }

        return true;
    }


    void SetupFloatingPointSystem()
    {
        // Add SgtFloatingCamera to the main camera
        if (!mainCamera.GetComponent<SgtFloatingCamera>())
        {
            mainCamera.gameObject.AddComponent<SgtFloatingCamera>();
        }
    }

    void VisualizeSystem(StarSystem system)
    {
        CreateStar(system.star);

        float lastOrbitRadius = 10f; // Starting orbit radius
        foreach (var body in system.celestialBodies)
        {
            CreateCelestialBody(body, ref lastOrbitRadius);
        }
    }

    void CreateStar(Star star)
    {
        GameObject starObj = new GameObject(star.name);
        starObj.transform.position = Vector3.zero; // Star at the center
        starObj.AddComponent<SgtFloatingObject>();

        Light light = starObj.AddComponent<Light>();
        light.type = LightType.Point;
        light.range = star.size * scaleFactor;
        light.color = star.color;

        if (atmospherePrefab != null)
        {
            SgtAtmosphere atmosphere = Instantiate(atmospherePrefab, starObj.transform);
            atmosphere.transform.localScale = Vector3.one * star.size * scaleFactor * 1.01f;
        }
        else
        {
            Debug.LogWarning("Atmosphere prefab is not assigned. Skipping atmosphere creation for the star.");
        }
    }

    void CreateCelestialBody(CelestialBody body, ref float lastOrbitRadius)
    {
        GameObject bodyObj = Instantiate(planetPrefab.gameObject);
        bodyObj.name = body.name;

        // Calculate position based on orbital radius
        float orbitRadius = body.orbitalRadius * orbitScaleFactor;
        Vector3 randomPosition = Random.insideUnitSphere.normalized * orbitRadius;
        randomPosition.y *= 0.1f; // Flatten the system a bit
        bodyObj.transform.position = randomPosition;

        bodyObj.AddComponent<SgtFloatingObject>();

        // Scale the body
        float bodyScale = body.size * scaleFactor;
        bodyObj.transform.localScale = Vector3.one * bodyScale;

        if (body.type == CelestialBodyType.GasGiant)
        {
            if (atmospherePrefab != null)
            {
                SgtAtmosphere atmosphere = Instantiate(atmospherePrefab, bodyObj.transform);
                atmosphere.transform.localScale = Vector3.one * bodyScale * 1.1f;
            }
        }

        if (body.type == CelestialBodyType.AsteroidBelt)
        {
            CreateAsteroidBelt(body, orbitRadius);
        }

        lastOrbitRadius = orbitRadius + bodyScale * 2; // Update last orbit radius
    }

    void CreateAsteroidBelt(CelestialBody belt, float orbitRadius)
    {
        if (asteroidBeltPrefab != null)
        {
            SgtBelt asteroidBelt = Instantiate(asteroidBeltPrefab);
            asteroidBelt.name = belt.name;
            asteroidBelt.transform.position = Vector3.zero;
            asteroidBelt.gameObject.AddComponent<SgtFloatingObject>();
            asteroidBelt.transform.localScale = Vector3.one * orbitRadius;
        }
        else
        {
            Debug.LogWarning("Asteroid belt prefab is not assigned. Skipping asteroid belt creation.");
        }
    }

    void SetupStarfield()
    {
        starfield.transform.position = Vector3.zero;
        starfield.transform.localScale = Vector3.one * 1000f;
        starfield.gameObject.AddComponent<SgtFloatingObject>(); // Add floating object component
    }

    void PlacePlayerShip()
    {
        if (syntySpaceshipPrefab != null)
        {
            GameObject playerShip = Instantiate(syntySpaceshipPrefab);
            playerShip.transform.position = new Vector3(0, 0, -50) * orbitScaleFactor; // Start position
            playerShip.AddComponent<SgtFloatingObject>();

            // Set up the camera to follow the player ship
            mainCamera.transform.SetParent(playerShip.transform);
            mainCamera.transform.localPosition = new Vector3(0, 5, -20); // Adjust as needed
        }
        else
        {
            Debug.LogWarning("Spaceship prefab is not assigned. Skipping player ship placement.");
        }
    }
}
