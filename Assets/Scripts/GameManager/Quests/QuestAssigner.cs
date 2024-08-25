using PixelCrushers.QuestMachine;
using UnityEngine;

namespace GameManager.Quests
{
    public class QuestAssigner : MonoBehaviour
    {
        public Quest quest; // Assign the quest from the inspector

        public void AssignQuest()
        {
            QuestJournal playerJournal = FindObjectOfType<QuestJournal>();
            if (playerJournal != null && quest != null)
            {
                playerJournal.AddQuest(quest);
            }

            // Set all quest nodes to Active
            foreach (var questNode in quest.nodeList)
            {
                questNode.SetState(QuestNodeState.Active);
            }
        }
    }
}
