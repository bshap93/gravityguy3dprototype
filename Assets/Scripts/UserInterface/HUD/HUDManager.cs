using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace UserInterface.HUD
{
    public class HUDManager : MonoBehaviour
    {
        [SerializeField] public GameObject hudContainer;

        private void Start()
        {
            if (hudContainer == null)
            {
                Debug.LogError("HUD Container is not assigned in the HUDManager. Please assign it in the Inspector.");
                enabled = false; // Disable this script if hudContainer is not set
                return;
            }

            // Subscribe to Dialogue System events
            DialogueManager.instance.conversationStarted += HideHUD;
            DialogueManager.instance.conversationEnded += ShowHUD;
        }

        private void OnDisable()
        {
            // Unsubscribe from Dialogue System events
            if (DialogueManager.instance != null)
            {
                DialogueManager.instance.conversationStarted -= HideHUD;
                DialogueManager.instance.conversationEnded -= ShowHUD;
            }
        }

        private void HideHUD(Transform actor)
        {
            if (hudContainer != null)
            {
                hudContainer.SetActive(false);
            }
        }

        private void ShowHUD(Transform actor)
        {
            if (hudContainer != null)
            {
                hudContainer.SetActive(true);
            }
        }
    }
}
