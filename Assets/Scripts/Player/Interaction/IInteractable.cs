using UnityEngine;

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
    void ShowInfo();
    void Interact();
    void StartDialogue();
}
