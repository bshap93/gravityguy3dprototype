using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Dialogue;
using Quests;
using UnityEngine;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static QuestManager QuestManager;
        public static DialogueManager DialogueManager;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            QuestManager = GetComponent<QuestManager>();
            DialogueManager = GetComponent<DialogueManager>();
        }

        [System.Serializable]
        public class SaveData
        {
            public List<Quest> activeQuests;
            public List<Quest> completedQuests;
        }

        public void SaveGame()
        {
            SaveData data = new SaveData
            {
                activeQuests = QuestManager.activeQuests,
                completedQuests = QuestManager.completedQuests
            };

            var formatter = new BinaryFormatter();

            var path = Application.persistentDataPath + "/save.dat";
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                formatter.Serialize(stream, data);
            }
        }

        public void LoadGame()
        {
            var path = Application.persistentDataPath + "/save.dat";
            if (!File.Exists(path)) return;
            var formatter = new BinaryFormatter();
            using var stream = new FileStream(path, FileMode.Open);
            if (formatter.Deserialize(stream) is SaveData data) QuestManager.activeQuests = data.activeQuests;
        }
    }
}
