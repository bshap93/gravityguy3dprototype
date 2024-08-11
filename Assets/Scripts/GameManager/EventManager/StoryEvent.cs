using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StoryEvent
{
    public string eventText;
    public List<Choice> choices;
}

[System.Serializable]
public class Choice
{
    public string choiceText;
    public List<Outcome> possibleOutcomes;
}

[System.Serializable]
public class Outcome
{
    public string outcomeText;
    public Dictionary<string, float> resourceChanges;
    public List<string> unlockedEvents;
}

public class StoryManager : MonoBehaviour
{
    public List<StoryEvent> allEvents;
    private List<StoryEvent> availableEvents;

    private void Start()
    {
        availableEvents = new List<StoryEvent>(allEvents);
    }

    public StoryEvent GetRandomEvent()
    {
        if (availableEvents.Count > 0)
        {
            int index = Random.Range(0, availableEvents.Count);
            return availableEvents[index];
        }

        return null;
    }

    public void ApplyOutcome(Outcome outcome)
    {
        // Apply resource changes and unlock new events
        // Implementation details here
    }
}
