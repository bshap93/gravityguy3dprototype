using System.Collections.Generic;
using Items;
using TMPro;
using UnityEngine;

namespace Quests
{
    public class QuestUI : MonoBehaviour
    {
        public TMP_Text questTitleText;
        public List<TMP_Text> objectivesText;
        public GameObject objectivesPanel;
        public ItemBasedObjectiveHandler objectiveHandler;


        private Dictionary<string, Quest> _quests;
        private string _currentQuestId;

        private void Start()
        {
            HideQuestInfo();

            if (objectiveHandler != null)
            {
                objectiveHandler.onObjectiveCompleted.AddListener(OnObjectiveCompleted);
            }
            else
            {
                Debug.LogError("ItemBasedObjectiveHandler reference is missing!");
            }
        }

        private void OnDestroy()
        {
            // Unsubscribe from the event when this component is destroyed
            if (objectiveHandler != null)
            {
                objectiveHandler.onObjectiveCompleted.RemoveListener(OnObjectiveCompleted);
            }
        }


        public void SetQuests(List<Quest> questsList)
        {
            this._quests = new Dictionary<string, Quest>();
            foreach (Quest quest in questsList)
            {
                this._quests.Add(quest.id, quest);
            }
        }

        public void StartQuest(string questId)
        {
            _currentQuestId = questId;
            ShowCurrentQuest(questId);
        }

        private void ShowCurrentQuest(string questId)
        {
            Quest currentQuest = _quests[questId];
            questTitleText.text = currentQuest.title;
            UpdateObjectivesText(currentQuest);
            ShowQuestInfo();
        }

        private void UpdateObjectivesText(Quest quest)
        {
            for (int i = 0; i < quest.objectives.Count; i++)
            {
                Objective objective = quest.objectives[i];
                string status = quest.completedObjectives.Contains(objective) ? "X" : "○";
                string text = $"{status} {objective.description}\n";
                objectivesText[i].text = text.Trim();
            }
        }

        private void OnObjectiveCompleted(string objectiveId)
        {
            if (_currentQuestId != null && _quests.TryGetValue(_currentQuestId, out var currentQuest))
            {
                Objective completedObjective = currentQuest.objectives.Find(o => o.id == objectiveId);

                if (completedObjective != null)
                {
                    currentQuest.completedObjectives.Add(completedObjective);
                    objectivesText[currentQuest.objectives.IndexOf(completedObjective)].text =
                        $"Xwd {completedObjective.description}";

                    UpdateObjectivesText(currentQuest);
                }
            }
        }

        private void HideQuestInfo()
        {
            questTitleText.gameObject.SetActive(false);
            objectivesPanel.SetActive(false);
        }

        private void ShowQuestInfo()
        {
            questTitleText.gameObject.SetActive(true);
            objectivesPanel.SetActive(true);
        }
    }
}
