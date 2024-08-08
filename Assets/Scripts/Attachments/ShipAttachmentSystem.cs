using System.Collections.Generic;
using UnityEngine;

public class ShipAttachmentSystem : MonoBehaviour
{
    public List<AttachmentSlot> attachmentSlots = new List<AttachmentSlot>();

    private void Start()
    {
        // Initialize slots
        attachmentSlots.AddRange(GetComponentsInChildren<AttachmentSlot>());
    }

    public void AttachUpgrade(Attachment attachment, string slotName)
    {
        AttachmentSlot slot = attachmentSlots.Find(s => s.slotName == slotName);
        if (slot != null)
        {
            slot.AttachUpgrade(attachment);
        }
        else
        {
            Debug.LogWarning($"No slot found with name {slotName}");
        }
    }

    public void RemoveAttachment(string slotName)
    {
        AttachmentSlot slot = attachmentSlots.Find(s => s.slotName == slotName);
        if (slot != null)
        {
            slot.RemoveAttachment();
        }
    }

    public void ActivateAttachment(string slotName)
    {
        AttachmentSlot slot = attachmentSlots.Find(s => s.slotName == slotName);
        if (slot != null)
        {
            slot.ActivateAttachment();
        }
    }

    public void DeactivateAttachment(string slotName)
    {
        AttachmentSlot slot = attachmentSlots.Find(s => s.slotName == slotName);
        if (slot != null)
        {
            slot.DeactivateAttachment();
        }
    }
}
