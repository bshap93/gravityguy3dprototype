using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class OdinDebugStartManager : SerializedMonoBehaviour
    {
        [System.Serializable]
        public class InventoryItem
        {
            [ValueDropdown("GetAllItemNames")] public string itemName;
            public int quantity;

#if UNITY_EDITOR
            private static List<string> GetAllItemNames()
            {
                // TODO: Replace this with a method to get all item names from Ultimate Crafting System
                // Example: return UltimateCraftingSystem.ItemDatabase.GetAllItemNames();
                return new List<string>() { "Item1", "Item2", "Item3" }; // Placeholder
            }
#endif
        }

        [System.Serializable]
        public class QuestState
        {
            [ValueDropdown("GetAllQuestNames")] public string questName;
            [ValueDropdown("GetQuestStates")] public string state;

#if UNITY_EDITOR
            private static List<string> GetAllQuestNames()
            {
                // Replace this with actual method to get all quest names from Dialogue System
                return new List<string>() { "Quest1", "Quest2", "Quest3" }; // Placeholder
            }

            private static List<string> GetQuestStates()
            {
                return new List<string>() { "Active", "Success", "Failure" };
            }
#endif
        }

        [System.Serializable]
        public class DebugStartPoint
        {
            [LabelText("Start Point Name")] public string name;

            [ValueDropdown("GetAllScenes")] [LabelText("Scene")]
            public string sceneName;

            [LabelText("Player Position")] public Vector3 position;

            [LabelText("Player Rotation")] public Vector3 rotation;

            [FoldoutGroup("Inventory")] [ListDrawerSettings(ShowIndexLabels = true)]
            public List<InventoryItem> inventoryItems = new List<InventoryItem>();

            [FoldoutGroup("Quests")] [ListDrawerSettings(ShowIndexLabels = true)]
            public List<QuestState> questStates = new List<QuestState>();

#if UNITY_EDITOR
            private static List<string> GetAllScenes()
            {
                List<string> scenes = new List<string>();
                foreach (UnityEditor.EditorBuildSettingsScene scene in UnityEditor.EditorBuildSettings.scenes)
                {
                    if (scene.enabled)
                    {
                        scenes.Add(System.IO.Path.GetFileNameWithoutExtension(scene.path));
                    }
                }

                return scenes;
            }
#endif
        }

        [ListDrawerSettings(ShowIndexLabels = true, AddCopiesLastElement = true)]
        public List<DebugStartPoint> debugStartPoints = new List<DebugStartPoint>();

        [Required("Assign the player GameObject or ensure it has the 'Player' tag")]
        public GameObject player;

        [Required("Assign the Ultimate Crafting System's InventoryManager component")]
        public MonoBehaviour
            inventoryManager; // Replace MonoBehaviour with the actual type from Ultimate Crafting System

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        [Button("Start From Selected Point", ButtonSizes.Large)]
        public void StartFromPoint(int index)
        {
            if (index < 0 || index >= debugStartPoints.Count)
            {
                Debug.LogError("Invalid debug start point index");
                return;
            }

            DebugStartPoint point = debugStartPoints[index];
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.LoadScene(point.sceneName);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            DebugStartPoint point = debugStartPoints.Find(p => p.sceneName == scene.name);
            if (point != null)
            {
                SetupPlayer(point);
                SetupInventory(point);
                SetupQuests(point);
            }
        }

        private void SetupPlayer(DebugStartPoint point)
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
            }

            if (player != null)
            {
                player.transform.position = point.position;
                player.transform.rotation = Quaternion.Euler(point.rotation);
            }
        }

        private void SetupInventory(DebugStartPoint point)
        {
            if (inventoryManager != null)
            {
                // Clear existing inventory
                // inventoryManager.ClearInventory(); // Uncomment and replace with actual method

                foreach (var item in point.inventoryItems)
                {
                    // Add item to inventory
                    // inventoryManager.AddItem(item.itemName, item.quantity); // Uncomment and replace with actual method
                    Debug.Log($"Adding {item.quantity} {item.itemName}(s) to inventory");
                }
            }
        }

        private void SetupQuests(DebugStartPoint point)
        {
            foreach (var quest in point.questStates)
            {
                // Adjust this based on how your quest system handles setting quest states
                QuestLog.SetQuestState(quest.questName, PixelCrushers.DialogueSystem.QuestState.Active);
            }
        }

        [Button("Add New Start Point", ButtonSizes.Medium)]
        private void AddNewStartPoint()
        {
            debugStartPoints.Add(
                new DebugStartPoint
                {
                    name = "New Start Point",
                    sceneName = SceneManager.GetActiveScene().name,
                    position = player != null ? player.transform.position : Vector3.zero,
                    rotation = player != null ? player.transform.rotation.eulerAngles : Vector3.zero
                });
        }
    }
}
