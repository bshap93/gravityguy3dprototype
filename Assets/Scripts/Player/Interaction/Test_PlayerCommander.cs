using System.Collections;
using System.Collections.Generic;
using Polyperfect.Crafting.Demo;
using UnityEngine;

public class Test_PlayerCommander : MonoBehaviour
{
    public void TellPlayerToInteract(BaseInteractable interactable)
    {
        var player = FindObjectOfType<Player_Interact>();
        player.StartInteracting(interactable);
    }

    public void TellPlayerToStopInteracting()
    {
        var player = FindObjectOfType<Player_Interact>();
        player.StopInteracting();
    }
}
