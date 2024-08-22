using UnityEngine;

namespace GameManager.Initializer
{
    public class GameInitializer : MonoBehaviour
    {
        public static GameInitializer Instance { get; private set; }

        [SerializeField] private string mainGameSceneName = "MainGame";
        [SerializeField] private string loadingSceneName = "LoadingScene";

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

        public void StartNewGame()
        {
            ES3.Save("isNewGame", true);
            LoadGame();
        }

        public void ContinueGame()
        {
            if (ES3.KeyExists("isNewGame"))
            {
                LoadGame();
            }
            else
            {
                Debug.LogWarning("No saved game found. Starting new game.");
                StartNewGame();
            }
        }

        private void LoadGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }
    }
}
