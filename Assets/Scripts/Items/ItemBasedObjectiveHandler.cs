using System.Collections.Generic;
using Polyperfect.Crafting.Framework;
using Polyperfect.Crafting.Integration;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Items
{
    public class ItemBasedObjectiveHandler : MonoBehaviour
    {
        public List<ChildSlotsInventory> trackedInventories = new List<ChildSlotsInventory>();
        public List<ItemObjective> activeItemObjectives;
        [FormerlySerializedAs("OnObjectiveCompleted")]
        public UnityEvent<string> onObjectiveCompleted = new UnityEvent<string>();

        private void Start()
        {
            // Subscribe to all tracked inventories
            foreach (var inventory in trackedInventories)
            {
                SubscribeToInventory(inventory);
            }
        }

        private void SubscribeToInventory(ChildSlotsInventory inventory)
        {
            foreach (var slot in inventory.Slots)
            {
                if (slot is ItemSlotComponent itemSlot)
                {
                    itemSlot.Changed += () => OnInventorySlotChanged(inventory);
                }
            }
        }

        private void UnsubscribeFromInventory(ChildSlotsInventory inventory)
        {
            foreach (var slot in inventory.Slots)
            {
                if (slot is ItemSlotComponent itemSlot)
                {
                    itemSlot.Changed -= () => OnInventorySlotChanged(inventory);
                }
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe from all tracked inventories
            foreach (var inventory in trackedInventories)
            {
                UnsubscribeFromInventory(inventory);
            }
        }

        public void AddInventoryToTrack(ChildSlotsInventory inventory)
        {
            if (!trackedInventories.Contains(inventory))
            {
                trackedInventories.Add(inventory);
                SubscribeToInventory(inventory);
            }
        }


        public void RemoveInventoryFromTracking(ChildSlotsInventory inventory)
        {
            if (trackedInventories.Contains(inventory))
            {
                UnsubscribeFromInventory(inventory);
                trackedInventories.Remove(inventory);
            }
        }


        private void OnInventorySlotChanged(ChildSlotsInventory changedInventory)
        {
            foreach (var slot in changedInventory.Slots)
            {
                if (slot is ItemSlotComponent itemSlot)
                {
                    var item = itemSlot.Peek();
                    if (!item.IsDefault())
                    {
                        CheckItemObjectives(item);
                    }
                }
            }
        }

        private void CheckItemObjectives(ItemStack item)
        {
            List<ItemObjective> completedObjectives = new List<ItemObjective>();

            foreach (var itemObjective in activeItemObjectives)
            {
                if (itemObjective.targetItemID == item.ID && !itemObjective.objective.isCompleted)
                {
                    itemObjective.currentAmount += item.Value.Value;
                    if (itemObjective.currentAmount >= itemObjective.requiredAmount)
                    {
                        CompleteObjective(itemObjective);
                        completedObjectives.Add(itemObjective);
                    }
                }
            }

            // Remove completed objectives after the iteration
            foreach (var completedObjective in completedObjectives)
            {
                activeItemObjectives.Remove(completedObjective);
            }
        }

        private void CompleteObjective(ItemObjective itemObjective)
        {
            itemObjective.objective.CompleteObjective();
            Debug.Log($"Completed objective: {itemObjective.objective.description}");

            onObjectiveCompleted.Invoke(itemObjective.objective.id);
        }

        public void AddItemObjective(string id, string description, RuntimeID targetItemID, int requiredAmount)
        {
            Objective newObjective = new Objective(id, description);
            ItemObjective newItemObjective = new ItemObjective(newObjective, targetItemID, requiredAmount);
            activeItemObjectives.Add(newItemObjective);
        }

        // Method to check total items across all inventories
        public int GetTotalItemCount(RuntimeID itemID)
        {
            int total = 0;
            foreach (var inventory in trackedInventories)
            {
                foreach (var slot in inventory.Slots)
                {
                    if (slot is ItemSlotComponent itemSlot)
                    {
                        var item = itemSlot.Peek();
                        if (item.ID == itemID)
                        {
                            total += item.Value.Value;
                        }
                    }
                }
            }

            return total;
        }
    }
}
