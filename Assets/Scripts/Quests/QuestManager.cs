using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        public List<Quest> activeQuests = new List<Quest>();
        public List<Quest> completedQuests = new List<Quest>();

        public void AddQuest(Quest quest)
        {
            activeQuests.Add(quest);
        }

        public void CompleteQuest(string questId)
        {
            var quest = activeQuests.Find(q => q.id == questId);
            if (quest == null) return;
            quest.isCompleted = true;
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
        }

        public void CompleteObjective(string questId, string objective)
        {
            var quest = activeQuests.Find(q => q.id == questId);
            if (quest == null) return;
            quest.CompleteObjective(objective);
            if (quest.isCompleted)
            {
                CompleteQuest(questId);
            }
        }

        public static object GetVariable(string variableName)
        {
            throw new System.NotImplementedException();
        }
        public static void SetVariable(string variableName, int newValue)
        {
            throw new System.NotImplementedException();
        }
    }
}
