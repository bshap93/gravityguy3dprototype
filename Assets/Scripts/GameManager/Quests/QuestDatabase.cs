using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    [CreateAssetMenu(fileName = "QuestDatabase", menuName = "Quests/QuestDatabase")]
    public class QuestDatabase : ScriptableObject
    {
        public List<Quest> allQuests;

        public Quest GetQuest(string questId)
        {
            return allQuests.Find(quest => quest.id == questId);
        }

        public List<Quest> GetAllQuests()
        {
            return allQuests;
        }
    }
}
