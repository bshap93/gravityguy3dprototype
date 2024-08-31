using Michsky.MUIP;
using TMPro;
using UnityEngine;

namespace Player.Interaction.Common
{
    public abstract class InteractiveMenu : MonoBehaviour
    {
        public GameObject menuPanel;
        public TMP_Text objectNameText;

        public GameObject player;
        public AudioSource interactiveMenuUISound;

        IInteractable _selectedObject;
        Camera _mainCamera;

        public AudioClip openingSound;
        public AudioClip closingSound;
        public AudioClip selectSound;
        public AudioClip tooFarSound;
        // Methods
        protected abstract bool HasDialogue();
        protected abstract bool HasInfo();
        protected abstract bool HasQuest();
        protected abstract bool HasTrade();
        protected abstract void ToggleSelectionOfMouse();
        public abstract void SelectObject(IInteractable interactable);


        public abstract void DeselectObject();
        public abstract void ShowInfo();
        public abstract void TradeAndExchange();
        public abstract void StartDialogue();
        public abstract void GetQuestInfo();
    }
}
