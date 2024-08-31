using Michsky.MUIP;
using PixelCrushers.DialogueSystem;
using Player.Interaction.Common;
using TMPro;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Player.Interaction.Nearby
{
    public class NearbyInteractiveMenu : InteractiveMenu
    {
        public ButtonManager infoButton;
        public ButtonManager exchangeItemsButton;
        public ButtonManager dialogueButton;


        private UnityEngine.Camera _mainCamera;
        private NearbyInteractable _selectedObject;
        InteractiveMenu _interactiveMenuImplementation;

        void Start()
        {
            _mainCamera = Camera.main;
            menuPanel.SetActive(false);

            infoButton.onClick.AddListener(ShowInfo);
            exchangeItemsButton.onClick.AddListener(TradeAndExchange);
            dialogueButton.onClick.AddListener(StartDialogue);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                ToggleSelectionOfMouse();
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                DeselectObject();
            }
        }
        protected override bool HasDialogue()
        {
            if (_selectedObject != null)
                return _selectedObject.HasDialogue();

            return false;
        }
        protected override bool HasInfo()
        {
            if (_selectedObject != null)
                return _selectedObject.HasInfo();

            return false;
        }
        protected override bool HasQuest()
        {
            if (_selectedObject != null)
                return _selectedObject.HasQuest();

            return false;
        }
        protected override bool HasTrade()
        {
            if (_selectedObject != null)
                return _selectedObject.HasTrade();

            return false;
        }
        protected override void ToggleSelectionOfMouse()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                NearbyInteractable interactable = hit.collider.GetComponent<NearbyInteractable>();
                if (interactable)
                    SelectObject(interactable);
                else
                    DeselectObject();
            }
            else
                DeselectObject();
        }
        public override void SelectObject(IInteractable interactable)
        {
            throw new System.NotImplementedException();
        }

        public void SelectObject(NearbyInteractable interactable)
        {
            if (DistanceUtility.IsWithinInteractionDistance(
                    player.transform, interactable.boxCollider.transform, interactable.interactableDistance))
            {
                _selectedObject = interactable;
                menuPanel.SetActive(true);
                objectNameText.SetText(interactable.GetName());

                infoButton.gameObject.SetActive(interactable.HasInfo());
                exchangeItemsButton.gameObject.SetActive(interactable.CanInteract());
                dialogueButton.gameObject.SetActive(interactable.HasDialogue());
                interactiveMenuUISound.PlayOneShot(openingSound);
            }
            else
            {
                float distance = DistanceUtility.CalculateDistance(
                    player.transform, interactable.boxCollider.transform);

                Debug.Log($"Object is {distance} units away, too far to interact.");
                interactiveMenuUISound.PlayOneShot(tooFarSound);
            }
        }

        public override void DeselectObject()
        {
            _selectedObject?.OnInteractionEnd();
            if (_selectedObject?.baseInteractable != null)
                _selectedObject.baseInteractable.EndInteract(gameObject);

            _selectedObject = null;
            menuPanel.SetActive(false);

            interactiveMenuUISound.PlayOneShot(closingSound);
        }

        public override void ShowInfo()
        {
            if (_selectedObject != null)
            {
                Debug.Log($"Showing info for {_selectedObject.interactableName}");
                _selectedObject.ShowInfo();
                interactiveMenuUISound.PlayOneShot(selectSound);
            }
        }

        public override void StartDialogue()
        {
            if (_selectedObject != null)
            {
                var selectedObjectConversation = _selectedObject.GetCurrentConversationName();
                Debug.Log("Starting dialogue with " + selectedObjectConversation);
                DialogueManager.StartConversation(selectedObjectConversation);
            }
            else
            {
                Debug.LogWarning("No interactable found for dialogue system!");
            }

            interactiveMenuUISound.PlayOneShot(selectSound);
        }
        public override void GetQuestInfo()
        {
            if (_selectedObject != null)
            {
            }
        }
        public override void TradeAndExchange()
        {
            if (_selectedObject?.baseInteractable != null)
                _selectedObject.baseInteractable.BeginInteract(gameObject);
            else
                Debug.LogWarning("No interactable found for crafting system!");

            interactiveMenuUISound.PlayOneShot(selectSound);
        }


        private void OnDestroy()
        {
            // Remove listeners to prevent potential memory leaks
            infoButton.onClick.RemoveListener(ShowInfo);
            exchangeItemsButton.onClick.RemoveListener(TradeAndExchange);
            dialogueButton.onClick.RemoveListener(StartDialogue);
        }
    }
}
