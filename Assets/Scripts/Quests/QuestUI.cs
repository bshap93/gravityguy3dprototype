using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Quests;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI questTitleText;
    public TextMeshProUGUI questDescriptionText;
    public TextMeshProUGUI objectivesText;
    public Button completeObjectiveButton;
    public Button nextQuestButton;
    public Button startQuestButton; // New button for starting the quest

    private List<Quest> quests;
    private int currentQuestIndex = -1; // Start at -1 to indicate no quest is active
    private int currentObjectiveIndex = 0;

    private void Start()
    {
        completeObjectiveButton.onClick.AddListener(CompleteCurrentObjective);
        nextQuestButton.onClick.AddListener(ShowNextQuest);
        startQuestButton.onClick.AddListener(StartQuest); // Add listener for the new button

        // Initially hide quest info and show only the start button
        HideQuestInfo();
        startQuestButton.gameObject.SetActive(true);
    }

    public void SetQuests(List<Quest> quests)
    {
        this.quests = quests;
    }

    private void StartQuest()
    {
        currentQuestIndex = 0;
        ShowCurrentQuest();
        startQuestButton.gameObject.SetActive(false); // Hide the start button
    }

    private void ShowCurrentQuest()
    {
        if (currentQuestIndex >= 0 && currentQuestIndex < quests.Count)
        {
            Quest currentQuest = quests[currentQuestIndex];
            questTitleText.text = currentQuest.title;
            questDescriptionText.text = currentQuest.description;
            UpdateObjectivesText(currentQuest);
            ShowQuestInfo();
        }
        else
        {
            HideQuestInfo();
            questTitleText.text = "No more quests";
            questDescriptionText.text = "";
            objectivesText.text = "";
        }
    }

    private void UpdateObjectivesText(Quest quest)
    {
        string text = "Objectives:\n";
        for (int i = 0; i < quest.objectives.Count; i++)
        {
            Objective objective = quest.objectives[i];
            string status = quest.completedObjectives.Contains(objective) ? "✓" : "○";
            text += $"{status} {objective}\n";
        }

        objectivesText.text = text;
    }

    private void CompleteCurrentObjective()
    {
        if (currentQuestIndex >= 0 && currentQuestIndex < quests.Count)
        {
            Quest currentQuest = quests[currentQuestIndex];
            if (currentObjectiveIndex < currentQuest.objectives.Count)
            {
                Objective objective = currentQuest.objectives[currentObjectiveIndex];
                QuestManager.Instance.CompleteObjective(currentQuest.id, objective.id);
                currentObjectiveIndex++;
                UpdateObjectivesText(currentQuest);

                if (currentQuest.isCompleted)
                {
                    Debug.Log($"Quest completed: {currentQuest.title}");
                    currentObjectiveIndex = 0;
                    currentQuestIndex++;
                    ShowCurrentQuest();
                }
            }
        }
    }

    private void ShowNextQuest()
    {
        currentQuestIndex++;
        currentObjectiveIndex = 0;
        ShowCurrentQuest();
    }

    private void HideQuestInfo()
    {
        questTitleText.gameObject.SetActive(false);
        questDescriptionText.gameObject.SetActive(false);
        objectivesText.gameObject.SetActive(false);
        completeObjectiveButton.gameObject.SetActive(false);
        nextQuestButton.gameObject.SetActive(false);
    }

    private void ShowQuestInfo()
    {
        questTitleText.gameObject.SetActive(true);
        questDescriptionText.gameObject.SetActive(true);
        objectivesText.gameObject.SetActive(true);
        completeObjectiveButton.gameObject.SetActive(true);
        nextQuestButton.gameObject.SetActive(true);
    }
}
