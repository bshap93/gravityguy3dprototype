using UnityEngine;

namespace Player.Interaction.Common
{
    public interface IInteractable
    {
        string GetName();
        Vector3 GetPosition();
        void OnHoverEnter();
        void OnHoverExit();
        void OnInteractionEnd();
        bool HasInfo();
        bool CanInteract();
        bool HasDialogue();
        bool HasQuest();
        bool HasTrade();
        void ShowInfo();
        void Interact();
        void StartDialogue();
    }
}
