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
        public static GameManager Instance;

        public QuestManager questManager;
        public DialogueManager dialogueManager;

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

            questManager = GetComponent<QuestManager>();
            dialogueManager = GetComponent<DialogueManager>();
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
                activeQuests = questManager.activeQuests,
                completedQuests = questManager.completedQuests
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
            if (formatter.Deserialize(stream) is SaveData data) questManager.activeQuests = data.activeQuests;
        }
    }
}
