using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager Instance { get; private set; }


        public DialogueUI dialogueUi;
        public QuestUI questUi;

        public List<Quest> activeQuests = new List<Quest>();
        public List<Quest> completedQuests = new List<Quest>();
        List<Quest> _allQuests;

        [FormerlySerializedAs("QuestDBTable")] [SerializeField]
        QuestDatabase questDBTable;

        void Start()
        {
            _allQuests = questDBTable.GetAllQuests();
        }
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }


        public void AddQuest(string questId)
        {
            var quest = questDBTable.GetQuest(questId);
            if (quest != null && !activeQuests.Contains(quest))
            {
                activeQuests.Add(quest);
                Debug.Log("Added quest: " + quest.title);
            }

            questUi.SetQuests(activeQuests);
            questUi.StartQuest(questId);
        }

        void CompleteQuest(string questId)
        {
            var quest = activeQuests.Find(q => q.id == questId);
            if (quest == null) return;
            quest.isCompleted = true;
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
        }

        public void CompleteObjective(string questId, string objectiveId)
        {
            var quest = activeQuests.Find(q => q.id == questId);
            if (quest == null) return;
            quest.CompleteObjective(objectiveId);
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
        public Quest GetQuest(string questId)
        {
            return questDBTable.GetQuest(questId);
        }

        public List<Quest> GetAllQuests()
        {
            return questDBTable.GetAllQuests();
        }

        void Cleanup()
        {
            activeQuests.Clear();
            completedQuests.Clear();
            Instance = null;
        }

        private void OnDestroy()
        {
            Cleanup();
        }
    }
}
