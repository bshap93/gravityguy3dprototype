using System.Collections.Generic;
using GameEntityObjects.Player.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameManager
{
    public class AttachmentManager : MonoBehaviour
    {
        [FormerlySerializedAs("playerShip")] public PlayerController playerController;
        [FormerlySerializedAs("AvailableAttachments")]
        public List<Attachment> availableAttachments;
        private int _currentAttachmentIndex = -1;

        void Start()
        {
            CycleAndAttachUpgrade();
            playerController.StopUsingAttachment();
            playerController.UseAttachment();
        }

        private void CycleAndAttachUpgrade()
        {
            if (availableAttachments.Count > 0)
            {
                _currentAttachmentIndex = (_currentAttachmentIndex + 1) % availableAttachments.Count;
                playerController.AttachUpgrade(availableAttachments[_currentAttachmentIndex]);
            }
            else
            {
                Debug.LogWarning("No attachments available to equip.");
            }
        }
    }
}
