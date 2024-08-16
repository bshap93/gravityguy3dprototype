using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

namespace Quests
{
    [System.Serializable]
    public class Quest
    {
        public string id;
        public string title;
        public string description;
        public bool isCompleted;
        public List<Objective> objectives;
        public List<Objective> completedObjectives;

        public Quest(string id, string title, string description, List<Objective> objectives)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.objectives = objectives;
            this.isCompleted = false;
            this.completedObjectives = new List<Objective>();
        }

        public void AddObjective(Objective objective)
        {
            objectives.Add(objective);
        }


        public void CompleteObjective(string objectiveId)
        {
            var objective = objectives.Find(o => o.id == objectiveId);
            if (objective == null) return;
            objective.CompleteObjective();
            if (!completedObjectives.Contains(objective))
            {
                completedObjectives.Add(objective);
                if (completedObjectives.Count == objectives.Count)
                {
                    isCompleted = true;
                }
            }
        }

        public void ClearCompletedObjectives()
        {
            completedObjectives.Clear();
            isCompleted = false;
        }
    }
}
