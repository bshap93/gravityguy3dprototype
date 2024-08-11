using System.Collections;
using System.Collections.Generic;
using Dialogue;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    DialogueManager _dialogueManager;


    // Start is called before the first frame update
    void Start()
    {
        _dialogueManager = DialogueManager.Instance;
    }
    public void InteractWithCaptain()
    {
        TalkToCaptain();
    }

    void TalkToCaptain()
    {
        // Start dialogue with the captain
        DialogueManager.StartDialog("dialogue1");

        // When objective is completed
        GameManager.GameManager.QuestManager.CompleteObjective("quest1", "Talk to the captain");
    }
}
