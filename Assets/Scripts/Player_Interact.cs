using System.Collections;
using System.Collections.Generic;
using Polyperfect.Crafting.Demo;
using UnityEngine;

public class Player_Interact : MonoBehaviour
{
    BaseInteractable currentlyInteractingWith;
    // Start is called before the first frame update
    public void StartInteracting(BaseInteractable interactable)
    {
        if (currentlyInteractingWith)
            StopInteracting();
        
        interactable.BeginInteract(this.gameObject);
        currentlyInteractingWith = interactable;
        
    }

    public void StopInteracting()
    {
        currentlyInteractingWith.EndInteract(this.gameObject);
        
    }
}
