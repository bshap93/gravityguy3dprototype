using System.Collections.Generic;
using Generation.Generators;
using Generation.Objects;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Generation.Managers
{
    public class SystemManager : MonoBehaviour
    {
        public static SystemManager Instance { get; private set; }

        public StarSystem currentSystem;
        public GameObject playerShip;
        public GameObject scienceShip;

        [System.Serializable]
        public class LocationScene
        {
            public string name;
            public string sceneName;
            public Vector3 position;
        }

        public List<LocationScene> locationScenes;

        private SystemGenerator generator;
        private string currentSceneName;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            generator = GetComponent<SystemGenerator>();
            GenerateOrLoadSystem();
            LoadMainSystemScene();
        }

        void GenerateOrLoadSystem()
        {
            if (ES3.KeyExists("starSystem"))
            {
                currentSystem = ES3.Load<StarSystem>("starSystem");
            }
            else
            {
                currentSystem = generator.GenerateTauCetiSystem();
            }
        }

        void LoadMainSystemScene()
        {
            SceneManager.LoadScene("MainSystemScene");
            currentSceneName = "MainSystemScene";
            PositionShips();
        }

        void PositionShips()
        {
            // Logic to position player and science ships
        }

        public void EnterLocation(string locationName)
        {
            LocationScene location = locationScenes.Find(loc => loc.name == locationName);
            if (location != null)
            {
                SaveCurrentSceneState();
                SceneManager.LoadScene(location.sceneName);
                currentSceneName = location.sceneName;
                // Position ships for this location
            }
        }

        public void ReturnToMainSystem()
        {
            SaveCurrentSceneState();
            LoadMainSystemScene();
        }

        void SaveCurrentSceneState()
        {
            // Save current scene state (ship positions, etc.)
        }

        public void SaveSystem()
        {
            ES3.Save("starSystem", currentSystem);
            ES3.Save("currentScene", currentSceneName);
            SaveCurrentSceneState();
        }

        public void LoadSystem()
        {
            if (ES3.KeyExists("starSystem"))
            {
                currentSystem = ES3.Load<StarSystem>("starSystem");
                string sceneToLoad = ES3.Load<string>("currentScene");
                SceneManager.LoadScene(sceneToLoad);
                currentSceneName = sceneToLoad;
                // Load ship positions
            }
        }
    }
}
