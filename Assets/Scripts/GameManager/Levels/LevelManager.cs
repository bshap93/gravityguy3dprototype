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
            Quest quest1 = new Quest(
                "1",
                "Freehold Bound",
                "Travel to Freehold to give them the Nitrogen and Printer parts",
                new System.Collections.Generic.List<string>
                {
                    "Obtain Nitrogen and Printer Parts",
                    "Travel to Freehold",
                    "Give Nitrogen to Freehold",
                    "Give Printer Parts to Freehold"
                }
            );
        }
    }
}
