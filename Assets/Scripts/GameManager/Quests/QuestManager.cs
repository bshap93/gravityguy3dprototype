﻿using PixelCrushers.DialogueSystem;
using UnityEngine;

namespace GameManager.Quests
{
    public class QuestManager : MonoBehaviour
    {
        // Instance 
        public static QuestManager Instance { get; private set; }

        // Awake method to set the instance
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("Multiple QuestManager instances found. Destroying duplicate.");
                Destroy(gameObject);
            }
        }
        // Method to check if entries 2 and 3 are successful and activate entries 4 and 5
        public void CheckAndActivateNextEntries(string questName)
        {
            // Check if both entries 2 and 3 are set to success
            if (QuestLog.GetQuestEntryState(questName, 2) == QuestState.Success &&
                QuestLog.GetQuestEntryState(questName, 3) == QuestState.Success)
            {
                // Set entries 4 and 5 to active
                QuestLog.SetQuestEntryState(questName, 4, QuestState.Active);
                QuestLog.SetQuestEntryState(questName, 5, QuestState.Active);

                Debug.Log($"Entries 4 and 5 of quest '{questName}' have been activated.");
            }
        }

        // Method to set a specific quest entry to success and check for next entries
        public void SetEntryToSuccess(string questName, int entryNumber)
        {
            // Set the specified entry to success
            QuestLog.SetQuestEntryState(questName, entryNumber, QuestState.Success);

            // Check if the next entries need to be activated
            CheckAndActivateNextEntries(questName);
        }
    }
}
