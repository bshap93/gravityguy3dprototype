using JetBrains.Annotations;
using UnityEngine;

namespace Player.PlayerController.Components
{
    public class AttachmentManager : MonoBehaviour
    {
        public Transform attachmentPoint;
        [CanBeNull] public Attachment currentAttachment;

        public void AttachUpgrade(Attachment attachmentPrefab)
        {
        }
        public void UseAttachment()
        {
        }
        public void StopUsingAttachment()
        {
        }
    }
}
