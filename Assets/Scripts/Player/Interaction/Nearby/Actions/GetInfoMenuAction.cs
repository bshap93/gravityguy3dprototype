using Player.Interaction.Common;
using UnityEngine;

namespace Player.Interaction.Nearby.Actions
{
    public class GetInfoMenuAction : MenuAction
    {
        private IInteractable _selectedObject;
        protected override void Execute()
        {
            if (_selectedObject != null)
            {
                Debug.Log($"Showing info for {_selectedObject.GetName()}");
                _selectedObject.ShowInfo();
            }
        }
    }
}
