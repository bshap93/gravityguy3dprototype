using UnityEngine;

namespace UserInterface
{
    public class UIManager : MonoBehaviour
    {
        public void OnSaveButtonClicked()
        {
            GameManager.GameManager.Instance.SaveGame();
        }

        public void OnLoadButtonClicked()
        {
            GameManager.GameManager.Instance.LoadGame();
        }
    }
}
