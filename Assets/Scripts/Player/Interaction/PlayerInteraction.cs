using System.Collections;
using System.Collections.Generic;
using Dialogue;
using GameManager.Dialogue;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    HomebrewDialogueManager _homebrewDialogueManager;


    // Start is called before the first frame update
    void Start()
    {
        _homebrewDialogueManager = HomebrewDialogueManager.Instance;
    }
    public void InteractWithCaptain()
    {
        TalkToCaptain();
    }

    void TalkToCaptain()
    {
        // Start dialogue with the captain
        HomebrewDialogueManager.StartDialog("dialogue1");

        // When objective is completed
        GameManager.GameManager.QuestManager.CompleteObjective("quest1", "Talk to the captain");
    }
}
