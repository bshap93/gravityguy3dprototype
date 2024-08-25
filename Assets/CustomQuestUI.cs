using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using PixelCrushers.DialogueSystem;
using UnityEngine.Serialization;

public class CustomQuestUI : MonoBehaviour
{
    public TMP_Text questTitleText; // Assign in inspector
    public TMP_Text objectivesText; // Assign in inspector
    public Image completedIcon; // Assign in inspector

    public GameObject checkMarkPrefab; // Assign this in the Inspector
    [SerializeField] bool isFinished;


    void Awake()
    {
        isFinished = false;

        GameObject checkMark = Instantiate(checkMarkPrefab, transform);

        checkMark.SetActive(false);
    }

    void UpdateQuestUI()
    {
        string questName = "Freehold Bound"; // Replace with your quest's actual name

        if (QuestLog.IsQuestActive(questName))
        {
            questTitleText.text = questName;

            int entryCount = QuestLog.GetQuestEntryCount(questName);
            string objectives = "";
            bool anyActiveEntries = false;

            for (int i = 1; i <= entryCount; i++)
            {
                QuestState entryState = QuestLog.GetQuestEntryState(questName, i);

                if (entryState == QuestState.Active)
                {
                    objectives += QuestLog.GetQuestEntry(questName, i) + "\n";
                    anyActiveEntries = true;
                }
            }

            objectivesText.text = objectives;

            // If no active entries, hide the objectives text and icon
            if (!anyActiveEntries)
            {
                objectivesText.text = "No active entries.";
                completedIcon.enabled = false;
            }
            else
            {
                completedIcon.enabled = true; // Adjust this based on specific logic if needed
            }
        }
        else
        {
            questTitleText.text = "No active quests";
            objectivesText.text = "";
            completedIcon.enabled = false;
        }
    }

    void Update()
    {
        UpdateQuestUI();
    }
}
