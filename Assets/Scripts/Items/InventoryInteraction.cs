using Polyperfect.Crafting.Integration;
using UnityEngine;

// Make sure to include the correct namespace

namespace Items
{
    public class InventoryInteraction : MonoBehaviour
    {
        public BaseItemStackInventory inventory;

        void Start()
        {
            // If not assigned in inspector, try to get it from this GameObject
            if (inventory == null)
                inventory = GetComponent<BaseItemStackInventory>();
        }

        // Methods to interact with the inventory...
    }
}
