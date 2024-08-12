using Quests;
using UnityEngine;

namespace GameManager.Levels
{
    public class LevelManager : MonoBehaviour
    {
        void Start()
        {
            InitializeQuestsAndDialogue();
        }

        void InitializeQuestsAndDialogue()
        {
            Quest debutQuest = new Quest(
                "1",
                "Freehold Bound",
                "Travel to Freehold to give them the Nitrogen and Printer parts",
                new System.Collections.Generic.List<Objective>
                {
                    new Objective("1_1", "Obtain Nitrogen and Printer Parts"),
                    new Objective("1_2", "Travel to Freehold"),
                    new Objective("1_3", "Give Nitrogen to Freehold"),
                    new Objective("1_4", "Give Printer Parts to Freehold"),
                }
            );
        }
    }
}
