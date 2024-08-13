using System.Collections.Generic;
using Dialogue;
using JetBrains.Annotations;
using Polyperfect.Crafting.Demo;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Player.Interaction
{
    public class InteractiveMenu : MonoBehaviour
    {
        public GameObject menuPanel;
        public Text objectNameText;
        public Button infoButton;
        [FormerlySerializedAs("exchangeItems")] [FormerlySerializedAs("actionButton")]
        public Button exchangeItemsButton;
        public Button dialogueButton;
        public float interactableDistance = 30f;
        public GameObject player;

        [FormerlySerializedAs("interactable")] [CanBeNull]
        private BaseInteractable _interactableForCraftingSystem;
        [CanBeNull] private IInteractable _gameInteractable;


        private Camera _mainCamera;
        private PhysicsRaycaster _physicsRaycaster;
        private MyInteractable _selectedObject;
        private MyInteractable _hoveredObject;


        void Start()
        {
            _mainCamera = Camera.main;
            _physicsRaycaster = _mainCamera.GetComponent<PhysicsRaycaster>();
            if (_physicsRaycaster == null)
            {
                Debug.LogError("PhysicsRaycaster not found on the main camera!");
            }

            menuPanel.SetActive(false);

            infoButton.onClick.AddListener(ShowInfo);
            exchangeItemsButton.onClick.AddListener(ExchangeItems);
            dialogueButton.onClick.AddListener(StartDialogue);
        }


        void Update()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerEventData, results);

            MyInteractable myInteractable = null;
            foreach (RaycastResult result in results)
            {
                myInteractable = result.gameObject.GetComponent<MyInteractable>();
                if (myInteractable != null)
                    break;
            }

            if (myInteractable != null)
            {
                if (Input.GetMouseButtonDown(0)) // Left mouse button
                {
                    SelectObject(myInteractable);
                }
            }
            else
            {
                if (Input.GetMouseButtonDown(0)) // Left mouse button
                {
                    DeselectObject();
                }
            }
        }


        public void SelectObject(MyInteractable obj)
        {
            // if distance is less than interactable distance
            var distance = Vector3.Distance(obj.transform.position, player.transform.position);
            if (distance > interactableDistance)
            {
                Debug.Log("Object is too far away!");
                return;
            }
            else
            {
                _selectedObject = obj;
                menuPanel.SetActive(true);
                objectNameText.text = obj.interactableName;
            }
        }

        public void DeselectObject()
        {
            _selectedObject = null;
            menuPanel.SetActive(false);
            if (_interactableForCraftingSystem != null)
                _interactableForCraftingSystem.EndInteract(gameObject);
            else
                Debug.LogWarning("No interactable found for crafting system!");
        }

        public void ToggleSelectedObject(MyInteractable obj)
        {
            if (_selectedObject != null)
            {
                DeselectObject();
            }
            else
            {
                SelectObject(obj);
            }
        }

        void ShowInfo()
        {
            if (_selectedObject != null)
            {
                Debug.Log($"Showing info for {_selectedObject.interactableName}");
                // Implement info display logic here
            }
        }

        void ExchangeItems()
        {
            if (_selectedObject != null)
            {
                if (_interactableForCraftingSystem != null)
                    _interactableForCraftingSystem.BeginInteract(gameObject);
                else
                    Debug.LogWarning("No interactable found for crafting system!");
            }
        }

        void StartDialogue()
        {
            DialogueManager.Instance.dialogueUi
                .StartConversation(_selectedObject.currentNextDialogueNode.id);
        }
    }
}
