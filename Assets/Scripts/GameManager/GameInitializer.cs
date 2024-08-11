using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameInitializer : MonoBehaviour
    {
        void Start()
        {
            if (GameManager.Instance == null)
            {
                GameObject gameManagerObject = new GameObject("GameManager");
                gameManagerObject.AddComponent<GameManager>();
            }

            // Load your main menu or first game scene
            // SceneManager.LoadScene("MainMenu");
        }
    }
}
