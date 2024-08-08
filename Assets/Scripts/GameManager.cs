using UnityEngine;
using System.Collections.Generic;
using Player;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [FormerlySerializedAs("playerShip")] public PlayerController playerController;
    [FormerlySerializedAs("AvailableAttachments")]
    public static List<Attachment> availableAttachments;
    private int _currentAttachmentIndex = -1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerController.UseAttachment();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerController.StopUsingAttachment();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            CycleAndAttachUpgrade();
        }
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
