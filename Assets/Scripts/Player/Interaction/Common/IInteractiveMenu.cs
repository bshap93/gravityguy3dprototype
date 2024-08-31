using Michsky.MUIP;
using TMPro;
using UnityEngine;

namespace Player.Interaction.Common
{
    public abstract class InteractiveMenu : MonoBehaviour
    {
        public GameObject MenuPanel;
        public TMP_Text ObjectNameText;

        public GameObject Player;
        public AudioSource InteractiveMenuUISound;


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

        IInteractable _selectedObject;
        Camera _mainCamera;
        bool _hasDialogue;
        bool _hasInfo;
        bool _hasQuest;
        bool _hasTrade;


        public void Start()
        {
            _mainCamera = Camera.main;
            MenuPanel.SetActive(false);
        }
        public abstract void DeselectObject();
        public abstract void ShowInfo();
        public abstract void TradeAndExchange();
        public abstract void StartDialogue();
        public abstract void GetQuestInfo();
    }
}
