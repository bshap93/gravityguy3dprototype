using UnityEngine;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;

public class QuestStateManager : MonoBehaviour
{
    [Title("Quest Configuration")]
    public string questName;

    [Title("Set Quest State")]

    [Button("Set Quest to Active")]
    public void SetQuestActive()
    {
        QuestLog.SetQuestState(questName, QuestState.Active);
    }

    [Button("Set Quest to Success")]
    public void SetQuestSuccess()
    {
        QuestLog.SetQuestState(questName, QuestState.Success);
    }

    [Button("Set Quest to Failure")]
    public void SetQuestFailure()
    {
        QuestLog.SetQuestState(questName, QuestState.Failure);
    }

    [Title("Set Quest Entry States")]

    [Button("Set Entry 1 to Active")]
    public void SetEntry1Active()
    {
        QuestLog.SetQuestEntryState(questName, 1, QuestState.Active);
    }

    [Button("Set Entry 1 to Success")]
    public void SetEntry1Success()
    {
        QuestLog.SetQuestEntryState(questName, 1, QuestState.Success);
    }

    [Button("Set Entry 2 to Active")]
    public void SetEntry2Active()
    {
        QuestLog.SetQuestEntryState(questName, 2, QuestState.Active);
    }

    [Button("Set Entry 2 to Success")]
    public void SetEntry2Success()
    {
        QuestLog.SetQuestEntryState(questName, 2, QuestState.Success);
    }

    [Button("Set Entry 3 to Active")]
    public void SetEntry3Active()
    {
        QuestLog.SetQuestEntryState(questName, 3, QuestState.Active);
    }

    [Button("Set Entry 3 to Success")]
    public void SetEntry3Success()
    {
        QuestLog.SetQuestEntryState(questName, 3, QuestState.Success);
    }

    [Button("Set Entry 4 to Active")]
    public void SetEntry4Active()
    {
        QuestLog.SetQuestEntryState(questName, 4, QuestState.Active);
    }

    [Button("Set Entry 4 to Success")]
    public void SetEntry4Success()
    {
        QuestLog.SetQuestEntryState(questName, 4, QuestState.Success);
    }

    [Button("Set Entry 5 to Active")]
    public void SetEntry5Active()
    {
        QuestLog.SetQuestEntryState(questName, 5, QuestState.Active);
    }

    [Button("Set Entry 5 to Success")]
    public void SetEntry5Success()
    {
        QuestLog.SetQuestEntryState(questName, 5, QuestState.Success);
    }

    [Button("Set Entry 6 to Active")]
    public void SetEntry6Active()
    {
        QuestLog.SetQuestEntryState(questName, 6, QuestState.Active);
    }

    [Button("Set Entry 6 to Success")]
    public void SetEntry6Success()
    {
        QuestLog.SetQuestEntryState(questName, 6, QuestState.Success);
    }

    // Add more buttons if needed for additional entries or quests
}
