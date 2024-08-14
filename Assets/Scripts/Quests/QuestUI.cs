using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using Quests;

public class QuestUI : MonoBehaviour
{
    public TMP_Text questTitleText;
    public List<TMP_Text> objectivesText;
    public GameObject objectivesPanel;


    private Dictionary<string, Quest> quests;
    private int currentObjectiveIndex = 0;

    private void Start()
    {
        // Initially hide quest info and show only the start button
    }

    public void SetQuests(List<Quest> quests)
    {
        this.quests = new Dictionary<string, Quest>();
        foreach (Quest quest in quests)
        {
            this.quests.Add(quest.id, quest);
        }
    }

    public void StartQuest(string questId)
    {
        currentObjectiveIndex = 0;
        ShowCurrentQuest(questId);
    }

    private void ShowCurrentQuest(string questId)
    {
        Quest currentQuest = quests[questId];
        questTitleText.text = currentQuest.title;
        UpdateObjectivesText(currentQuest);
        ShowQuestInfo();
        for (var i = 0; i < objectivesText.Count; i++)
        {
            objectivesText[i].text = "";
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
            objectivesText[i].text = text;
        }

        // objectivesText.text = text;
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
