using Michsky.MUIP;
using UnityEngine;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using TMPro;

namespace Player.Interaction
{
    public class InteractiveMenu : MonoBehaviour
    {
        public GameObject menuPanel;
        public TMP_Text objectNameText;
        public ButtonManager infoButton;
        public ButtonManager exchangeItemsButton;
        public ButtonManager dialogueButton;
        public ButtonManager questInfoButton;
        public GameObject player;
        public AudioSource interactiveMenuUISound;

        public AudioClip openingSound;
        public AudioClip closingSound;
        public AudioClip selectSound;
        public AudioClip tooFarSound;

        private UnityEngine.Camera _mainCamera;
        private MyInteractable _selectedObject;

        void Start()
        {
            _mainCamera = UnityEngine.Camera.main;
            menuPanel.SetActive(false);

            infoButton.onClick.AddListener(ShowInfo);
            exchangeItemsButton.onClick.AddListener(TradeAndExchange);
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

        public void DeselectObject()
        {
            _selectedObject?.OnInteractionEnd();
            if (_selectedObject?.baseInteractable != null)
                _selectedObject.baseInteractable.EndInteract(gameObject);

            _selectedObject = null;
            menuPanel.SetActive(false);

            interactiveMenuUISound.PlayOneShot(closingSound);
        }

        public void ShowInfo()
        {
            if (_selectedObject != null)
            {
                Debug.Log($"Showing info for {_selectedObject.interactableName}");
                _selectedObject.ShowInfo();
                interactiveMenuUISound.PlayOneShot(selectSound);
            }
        }

        public void StartDialogue()
        {
            if (_selectedObject != null)
            {
                var selectedObjectConversation = _selectedObject.GetCurrentConversationName();
                DialogueManager.StartConversation(selectedObjectConversation);
            }

            interactiveMenuUISound.PlayOneShot(selectSound);
        }
        public void GetQuestInfo()
        {
            if (_selectedObject != null)
            {
            }
        }
        public void TradeAndExchange()
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
