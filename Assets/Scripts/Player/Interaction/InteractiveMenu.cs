using UnityEngine;
using UnityEngine.UI;
using Dialogue;
using GameManager.Dialogue;
using PixelCrushers.DialogueSystem;

namespace Player.Interaction
{
    public class InteractiveMenu : MonoBehaviour
    {
        public GameObject menuPanel;
        public Text objectNameText;
        public Button infoButton;
        public Button exchangeItemsButton;
        public Button dialogueButton;
        public GameObject player;

        private Camera _mainCamera;
        private MyInteractable _selectedObject;

        void Start()
        {
            _mainCamera = Camera.main;
            menuPanel.SetActive(false);

            infoButton.onClick.AddListener(ShowInfo);
            exchangeItemsButton.onClick.AddListener(ExchangeItems);
            dialogueButton.onClick.AddListener(StartDialogue);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    MyInteractable interactable = hit.collider.GetComponent<MyInteractable>();
                    if (interactable)
                        SelectObject(interactable);
                    else
                        DeselectObject();
                }
                else
                    DeselectObject();
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                DeselectObject();
            }
        }

        public void SelectObject(MyInteractable interactable)
        {
            if (DistanceUtility.IsWithinInteractionDistance(
                    player.transform, interactable.boxCollider.transform, interactable.interactableDistance))
            {
                _selectedObject = interactable;
                menuPanel.SetActive(true);
                objectNameText.text = interactable.GetName();

                infoButton.gameObject.SetActive(interactable.HasInfo());
                exchangeItemsButton.gameObject.SetActive(interactable.CanInteract());
                dialogueButton.gameObject.SetActive(interactable.HasDialogue());
            }
            else
            {
                float distance = DistanceUtility.CalculateDistance(
                    player.transform, interactable.boxCollider.transform);

                Debug.Log($"Object is {distance} units away, too far to interact.");
            }
        }

        public void DeselectObject()
        {
            _selectedObject?.OnInteractionEnd();
            if (_selectedObject?.baseInteractable != null)
                _selectedObject.baseInteractable.EndInteract(gameObject);

            _selectedObject = null;
            menuPanel.SetActive(false);
        }

        void ShowInfo()
        {
            if (_selectedObject != null)
            {
                Debug.Log($"Showing info for {_selectedObject.interactableName}");
                _selectedObject.ShowInfo();
            }
        }

        void ExchangeItems()
        {
            if (_selectedObject?.baseInteractable != null)
                _selectedObject.baseInteractable.BeginInteract(gameObject);
            else
                Debug.LogWarning("No interactable found for crafting system!");
        }

        void StartDialogue()
        {
            if (_selectedObject != null)
            {
                var selectedObjectConversation = _selectedObject.GetCurrentConversationName();
                DialogueManager.StartConversation(selectedObjectConversation);
            }
        }

        private void OnDestroy()
        {
            // Remove listeners to prevent potential memory leaks
            infoButton.onClick.RemoveListener(ShowInfo);
            exchangeItemsButton.onClick.RemoveListener(ExchangeItems);
            dialogueButton.onClick.RemoveListener(StartDialogue);
        }
    }
}
