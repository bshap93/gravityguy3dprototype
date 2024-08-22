using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace GameManager.Initializer
{
    public class InitialSceneGenerator : MonoBehaviour
    {
        [System.Serializable]
        public class GameWorldData
        {
            public string starName;
            public List<string> planetNames;
        }

        private GameWorldData _gameWorldData = new GameWorldData();

        private void Start()
        {
            if (ES3.KeyExists("isNewGame") && ES3.Load<bool>("isNewGame"))
            {
                GenerateNewWorld();
            }
            else
            {
                LoadWorld();
            }
        }

        private void GenerateNewWorld()
        {
            _gameWorldData.starName = CelestialNameGenerator.GenerateStarName();
            _gameWorldData.planetNames = new List<string>();
            for (int i = 0; i < Random.Range(1, 5); i++)
            {
                _gameWorldData.planetNames.Add(CelestialNameGenerator.GeneratePlanetName());
            }


            SaveWorld();
            ES3.Save("isNewGame", false);
        }

        private void LoadWorld()
        {
            if (ES3.KeyExists("worldData"))
            {
                _gameWorldData = ES3.Load<GameWorldData>("worldData");
                Debug.Log("World loaded successfully.");
            }
            else
            {
                Debug.LogWarning("No world data found. Generating new world.");
                GenerateNewWorld();
            }
        }

        private void SaveWorld()
        {
            ES3.Save("worldData", _gameWorldData);
            Debug.Log("World saved successfully.");
        }

        public void SaveGame()
        {
            SaveWorld();
        }
    }
}
