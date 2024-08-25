using PixelCrushers.QuestMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameManager.Quests
{
    public class QuestHUD : MonoBehaviour
    {
        public TMP_Text questTitleText; // Assign in inspector
        public TMP_Text objectiveText; // Assign in inspector
        public GameObject listView;
        public GameObject questNodeListingPrefab;
        public GameObject list;


        private QuestJournal _questJournal;

        void Start()
        {
            _questJournal = FindObjectOfType<QuestJournal>();

            if (_questJournal != null)
            {
                UpdateQuestHUD();
            }
        }

        void UpdateQuestHUD()
        {
            if (_questJournal == null || _questJournal.questList.Count == 0)
            {
                questTitleText.text = "No active quests";
                objectiveText.text = "";
                return;
            }

            // Get the first active quest
            Quest firstQuest = _questJournal.questList[0];
            questTitleText.text = firstQuest.name;

            // Display the objectives of the first quest
            string objectives = "";
            foreach (var questNode in firstQuest.nodeList)
            {
                objectives += "- " + questNode.GetState() + " " + questNode.internalName + "\n";
            }

            objectiveText.text = objectives;
        }

        void Update()
        {
            // Optional: Update the HUD regularly or based on specific triggers
            UpdateQuestHUD();
        }
    }
}
