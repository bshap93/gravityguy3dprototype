using System;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    [System.Serializable]
    public class Quest
    {
        public string id;
        public string title;
        public string description;
        public bool isCompleted;
        public List<string> objectives;
        public List<string> completedObjectives;

        public Quest(string id, string title, string description, List<string> objectives)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.objectives = objectives;
            this.isCompleted = false;
            this.completedObjectives = new List<string>();
        }

        public void AddObjective(string objective)
        {
            objectives.Add(objective);
        }


        public void CompleteObjective(string objective)
        {
            if (objectives.Contains(objective) && !completedObjectives.Contains(objective))
            {
                completedObjectives.Add(objective);
                if (completedObjectives.Count == objectives.Count)
                {
                    isCompleted = true;
                }
            }
        }
    }
}
