using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using Polyperfect.Crafting.Integration;

public class InventoryDialogueBridge : MonoBehaviour
{
    // Reference to the player's inventory from Ultimate Crafting System
    public List<UGUITransferableItemSlot> playerInventory;

    private void Start()
    {
        if (playerInventory == null)
        {
            Debug.LogError("Player Inventory not assigned in InventoryDialogueBridge!");
            return;
        }


    }

    public void OnInventoryChanged()
    {

    }
}


