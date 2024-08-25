using UnityEngine;
using TMPro;
using PixelCrushers.DialogueSystem;

public class SimpleQuestObjectiveDisplay : MonoBehaviour
{
    public TMP_Text objectiveText;
    public int maxObjectives = 2;

    private void Update()
    {
        UpdateObjectiveDisplay();
    }

    private void UpdateObjectiveDisplay()
    {
        string displayText = "";
        int objectivesShown = 0;

        // Get all active quests
        string[] activeQuests = QuestLog.GetAllQuests();

        foreach (string questName in activeQuests)
        {
            // Get the number of entries for this quest
            int entryCount = QuestLog.GetQuestEntryCount(questName);

            for (int i = 1; i <= entryCount; i++)
            {
                // Check if the entry is active
                if (QuestLog.GetQuestEntryState(questName, i) == QuestState.Active)
                {
                    string entryText = QuestLog.GetQuestEntry(questName, i);
                    displayText += $"• {entryText}\n";
                    objectivesShown++;

                    if (objectivesShown >= maxObjectives)
                    {
                        break;
                    }
                }
            }

            if (objectivesShown >= maxObjectives)
            {
                break;
            }
        }

        objectiveText.text = displayText.TrimEnd('\n');
    }
}
