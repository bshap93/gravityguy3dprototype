using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using Player.Interaction.Common;
using Polyperfect.Crafting.Demo;
using UnityEngine;

namespace Player.Interaction.Nearby
{
    [System.Serializable]
    public class NearbyInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] public string interactableName;
        public BaseInteractable baseInteractable;
        [SerializeField] public Collider boxCollider;
        [SerializeField] public float interactableDistance;
        [SerializeField] List<string> conversationNames;

        NearbyInteractiveMenu _nearbyInteractiveMenu;

        public string GetCurrentConversationName()
        {
            return conversationNames[0];
        }

        void Awake()
        {
            baseInteractable = GetComponent<BaseInteractable>();
            _nearbyInteractiveMenu = FindObjectOfType<NearbyInteractiveMenu>();
        }

        public string GetName() => interactableName;

        public Vector3 GetPosition() => boxCollider ? boxCollider.transform.position : transform.position;

        public void OnMouseDown()
        {
            if (_nearbyInteractiveMenu != null)
                _nearbyInteractiveMenu.SelectObject(this);
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
        public bool HasDialogue()
        {
            // Implement actual logic 
            return true;
        }
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
            _nearbyInteractiveMenu = null;
        }

        void OnTriggerEnter(Collider other)
        {
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
