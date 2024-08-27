using System;
using System.Collections.Generic;
using Items;
using JetBrains.Annotations;
using PixelCrushers.DialogueSystem;
using Polyperfect.Crafting.Demo;
using UnityEngine;

namespace Player.Interaction
{
    [System.Serializable]
    public class MyInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] public string interactableName;
        public BaseInteractable baseInteractable;
        [SerializeField] public string defaultDialogueNodeId;
        [SerializeField] public string currentNextDialogueNodeId;
        [SerializeField] public BoxCollider boxCollider;
        [SerializeField] public float interactableDistance = 30f;
        [SerializeField] List<string> conversationNames;

        private InteractiveMenu _interactiveMenu;
        private Transform playerTransform;
        [CanBeNull] InteractableRangeEntered interactableRangeEntered;

        void Start()
        {
            // if component is not found, add it
            if (interactableRangeEntered == null)


                interactableRangeEntered = GetComponent<InteractableRangeEntered>();
        }

        public string GetCurrentConversationName()
        {
            return conversationNames[0];
        }

        void Awake()
        {
            baseInteractable = GetComponent<BaseInteractable>();
            currentNextDialogueNodeId = defaultDialogueNodeId;
            _interactiveMenu = FindObjectOfType<InteractiveMenu>();

            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        public string GetName() => interactableName;

        public Vector3 GetPosition() => boxCollider ? boxCollider.transform.position : transform.position;

        public void OnMouseDown()
        {
            if (_interactiveMenu != null)
                _interactiveMenu.SelectObject(this);
        }


        public void OnHoverEnter()
        {
            /* Implement if needed */
        }
        public void OnHoverExit()
        {
            /* Implement if needed */
        }
        public void OnInteractionEnd()
        {
            /* Implement if needed */
        }
        public bool HasInfo() => true; // Implement actual logic
        public bool CanInteract() => baseInteractable != null;
        public bool HasDialogue() => !string.IsNullOrEmpty(currentNextDialogueNodeId);
        public void ShowInfo()
        {
            /* Implement if needed */
        }
        public void Interact()
        {
            /* Implement if needed */
        }
        public void StartDialogue()
        {
            /* Implement if needed */
        }

        private void OnDestroy()
        {
            // Clean up any resources if needed
            baseInteractable = null;
            _interactiveMenu = null;
        }

        void OnTriggerEnter(Collider other)
        {
            if (interactableRangeEntered != null)
                // Check if the collider belongs to the player
                if (other.CompareTag("Player") && playerTransform != null)
                {
                    float distance = Vector3.Distance(playerTransform.position, transform.position);

                    // Check if the player is within interaction range
                    if (distance <= interactableDistance)
                    {
                        // Set the quest entry to success

                        Debug.Log("Player is within interaction range.");
                    }
                }
        }

        private void SetQuestEntryToState(string questName, int entryNumber,
            QuestState questState = QuestState.Success)

        {
            // Set the specified quest entry to success
            QuestLog.SetQuestEntryState(questName, entryNumber, QuestState.Success);
            Debug.Log($"Quest entry {entryNumber} for quest '{questName}' set to success.");
        }
    }
}
