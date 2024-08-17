using SpaceGraphicsToolkit;
using SpaceGraphicsToolkit.Starfield;
using UnityEngine;

namespace Generation.Visualizers
{
    public class SceneVisualizer : MonoBehaviour
    {
        public SgtFloatingCamera floatingCamera;
        public SgtStarfield starfield;

        [Header("Celestial Bodies")] public GameObject gasGiantPrefab;
        public GameObject asteroidFieldPrefab;

        [Header("Structures")] public GameObject generationShipPrefab;
        public GameObject asteroidSettlementPrefab;

        [Header("Player")] public GameObject playerShipPrefab;

        void Start()
        {
            SetupScene();
        }

        void SetupScene()
        {
            SetupBackground();
            CreateGasGiant();
            CreateAsteroidField();
            CreateGenerationShip();
            CreateAsteroidSettlement();
            PlacePlayerShip();
        }

        void SetupBackground()
        {
            // Setup starfield
            if (starfield != null)
            {
                starfield.transform.localScale = Vector3.one * 1000f;
            }
        }

        void CreateGasGiant()
        {
            if (gasGiantPrefab != null)
            {
                GameObject gasGiant = Instantiate(gasGiantPrefab, new Vector3(0, 0, 500), Quaternion.identity);
                gasGiant.AddComponent<SgtFloatingObject>();
            }
        }

        void CreateAsteroidField()
        {
            if (asteroidFieldPrefab != null)
            {
                GameObject asteroidField = Instantiate(
                    asteroidFieldPrefab, new Vector3(200, 0, 300), Quaternion.identity);

                asteroidField.AddComponent<SgtFloatingObject>();
            }
        }

        void CreateGenerationShip()
        {
            if (generationShipPrefab != null)
            {
                GameObject genShip = Instantiate(generationShipPrefab, new Vector3(0, 0, 100), Quaternion.identity);
                genShip.AddComponent<SgtFloatingObject>();
            }
        }

        void CreateAsteroidSettlement()
        {
            if (asteroidSettlementPrefab != null)
            {
                GameObject settlement = Instantiate(
                    asteroidSettlementPrefab, new Vector3(150, 0, 200), Quaternion.identity);

                settlement.AddComponent<SgtFloatingObject>();
            }
        }

        void PlacePlayerShip()
        {
            if (playerShipPrefab != null)
            {
                GameObject playerShip = Instantiate(playerShipPrefab, new Vector3(0, 0, 0), Quaternion.identity);
                playerShip.AddComponent<SgtFloatingObject>();

                // Setup camera
                if (floatingCamera != null)
                {
                    floatingCamera.transform.SetParent(playerShip.transform);
                    floatingCamera.transform.localPosition = new Vector3(0, 5, -20);
                }
            }
        }
    }
}
