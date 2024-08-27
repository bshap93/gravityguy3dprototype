using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Utilities
{
    public class QuestSaveManager : MonoBehaviour
    {
        public string questName;

        public void SaveQuestState()
        {
            QuestState state = QuestLog.GetQuestState(questName);
            ES3.Save(questName + "_State", state);

            int entryCount = Mathf.Min(QuestLog.GetQuestEntryCount(questName), 6);
            for (int i = 1; i <= entryCount; i++)
            {
                QuestState entryState = QuestLog.GetQuestEntryState(questName, i);
                ES3.Save(questName + "_Entry_" + i + "_State", entryState);
            }

            Debug.Log($"Saved quest state for {questName}");
        }

        public void LoadQuestState()
        {
            if (ES3.KeyExists(questName + "_State"))
            {
                QuestState state = ES3.Load<QuestState>(questName + "_State");
                QuestLog.SetQuestState(questName, state);

                int entryCount = Mathf.Min(QuestLog.GetQuestEntryCount(questName), 6);
                for (int i = 1; i <= entryCount; i++)
                {
                    if (ES3.KeyExists(questName + "_Entry_" + i + "_State"))
                    {
                        QuestState entryState = ES3.Load<QuestState>(questName + "_Entry_" + i + "_State");
                        QuestLog.SetQuestEntryState(questName, i, entryState);
                    }
                }

                Debug.Log($"Loaded quest state for {questName}");
            }
            else
            {
                Debug.LogWarning($"No saved state found for {questName}");
            }
        }

        [Button("Save Quest State")]
        public void SaveQuest()
        {
            SaveQuestState();
        }

        [Button("Load Quest State")]
        public void LoadQuest()
        {
            LoadQuestState();
        }

        // Add more methods for other quests if needed
    }
}
