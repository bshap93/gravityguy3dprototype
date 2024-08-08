using UnityEngine;

public class AttachmentSlot : MonoBehaviour
{
    public string slotName;
    public Attachment currentAttachment;

    public bool IsOccupied => currentAttachment != null;

    public void AttachUpgrade(Attachment attachment)
    {
        if (IsOccupied)
        {
            Debug.LogWarning($"Slot {slotName} is already occupied. Removing old attachment.");
            RemoveAttachment();
        }

        currentAttachment = attachment;
        currentAttachment.transform.SetParent(transform);
        currentAttachment.transform.localPosition = Vector3.zero;
        Debug.Log($"Attached {attachment.attachmentName} to slot {slotName}");
    }

    public void RemoveAttachment()
    {
        if (IsOccupied)
        {
            Destroy(currentAttachment.gameObject);
            currentAttachment = null;
            Debug.Log($"Removed attachment from slot {slotName}");
        }
    }

    public void ActivateAttachment()
    {
        if (IsOccupied)
        {
            currentAttachment.Activate();
        }
    }

    public void DeactivateAttachment()
    {
        if (IsOccupied)
        {
            currentAttachment.Deactivate();
        }
    }
}
