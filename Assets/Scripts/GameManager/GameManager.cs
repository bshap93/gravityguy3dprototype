using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Dialogue;
using GameManager.Dialogue;
using Quests;
using UnityEngine;

namespace GameManager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public static QuestManager QuestManager;
        public static HomebrewDialogueManager HomebrewDialogueManager;

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
            HomebrewDialogueManager = GetComponent<HomebrewDialogueManager>();
        }

        [System.Serializable]
        public class SaveData
        {
            public List<Quest> activeQuests;
            public List<Quest> completedQuests;
            public string playerName;
        }

        public void SaveGame()
        {
            SaveData data = new SaveData
            {
                activeQuests = QuestManager.activeQuests,
                completedQuests = QuestManager.completedQuests,
                playerName = HomebrewDialogueManager.playerName
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
            if (formatter.Deserialize(stream) is SaveData data)
            {
                QuestManager.activeQuests = data.activeQuests;
                QuestManager.completedQuests = data.completedQuests;
                HomebrewDialogueManager.playerName = data.playerName;
            }
        }
    }
}
