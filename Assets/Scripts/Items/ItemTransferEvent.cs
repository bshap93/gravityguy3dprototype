using UnityEngine;

namespace Items
{
    public class ItemTransferEvent : MonoBehaviour
    {
        // Define the delegate and event
        public delegate void ItemTransferredHandler(string itemName, int quantity);
        public static event ItemTransferredHandler OnItemTransferred;

        // Method to call when an item is transferred
        public void TransferItem(string itemName, int quantity)
        {
            // Emit the event if there are subscribers
            OnItemTransferred?.Invoke(itemName, quantity);
        }
    }
}
