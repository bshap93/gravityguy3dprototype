using System;
using Dialogue;
using JetBrains.Annotations;
using Polyperfect.Crafting.Demo;
using UnityEngine;

namespace Player.Interaction
{
    [System.Serializable]
    public class MyInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] public string interactableName;
        [CanBeNull] BaseInteractable _baseInteractable;

        [SerializeField] DialogueNode defaultDialogueNode;

        [SerializeField] public DialogueNode currentNextDialogueNode;

        [SerializeField] public Transform anchor;


        void Start()
        {
            _baseInteractable = GetComponent<BaseInteractable>();
            currentNextDialogueNode = defaultDialogueNode;
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }
        public Vector3 GetPosition()
        {
            throw new System.NotImplementedException();
        }
        public void OnHoverEnter()
        {
            throw new System.NotImplementedException();
        }
        public void OnHoverExit()
        {
            throw new System.NotImplementedException();
        }
        public void OnInteractionEnd()
        {
            throw new System.NotImplementedException();
        }
        public bool HasInfo()
        {
            throw new System.NotImplementedException();
        }
        public bool CanInteract()
        {
            throw new System.NotImplementedException();
        }
        public bool HasDialogue()
        {
            throw new System.NotImplementedException();
        }
        public void ShowInfo()
        {
            throw new System.NotImplementedException();
        }
        public void Interact()
        {
            throw new System.NotImplementedException();
        }
        public void StartDialogue()
        {
            throw new System.NotImplementedException();
        }
    }
}
