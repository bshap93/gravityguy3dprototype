using System.Collections.Generic;
using GameManager.Quests;
using PixelCrushers.DialogueSystem;
using Polyperfect.Crafting.Framework;
using Polyperfect.Crafting.Integration;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    public class FreeholdAsteroidBaseInventoryController : MonoBehaviour
    {
        public Dictionary<string, RuntimeID> ItemDictionary = new Dictionary<string, RuntimeID>();
        public Dictionary<string, string> QuestDictionary = new Dictionary<string, string>();
        public Dictionary<string, int> QuestEntryDictionary = new Dictionary<string, int>();
        public static FreeholdAsteroidBaseInventoryController Instance { get; private set; }
        public GameObject[] inventorySlots;
        public List<ItemObjective> ItemObjectives = new List<ItemObjective>();
        // Start is called before the first frame update

        public class ItemObjective
        {
            [SerializeField] public string QuestName;
            [SerializeField] public string ItemName;
            [SerializeField] public RuntimeID ItemId;
            [SerializeField] public int ItemQuantity;
            [SerializeField] public QuestEntryArgs QuestEntryArgs;
            public ItemObjective(RuntimeID itemId)
            {
                ItemId = itemId;
            }
        }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogWarning("Multiple Freehold Drop box inventor instances found. Destroying duplicate.");
                Destroy(gameObject);
            }
        }
        void Start()
        {
            ItemDictionary.Add("Nitrogen Pellet", new RuntimeID(-1424567977388441138));
            ItemDictionary.Add("3D Printer", new RuntimeID(3464810275771887701));

            QuestDictionary.Add("Nitrogen Pellet", "Freehold Bound");
            QuestDictionary.Add("3D Printer", "Freehold Bound");

            QuestEntryDictionary.Add("Nitrogen Pellet", 4);
            QuestEntryDictionary.Add("3D Printer", 5);

            // Register the method to Lua
            Lua.RegisterFunction(
                "AddItemObjective", this,
                SymbolExtensions.GetMethodInfo(
                    () => FreeholdAsteroidBaseInventoryController.AddItemObjectiveWrapper(null)));
        }

        public void AddItemObjective(ItemObjective itemObjective)
        {
            ItemObjectives.Add(itemObjective);
            Debug.Log($"Objective added: {itemObjective.ItemId}, Quantity: {itemObjective.ItemQuantity}");
        }

        // Wrapper method for Lua
        public static void AddItemObjectiveWrapper(string itemName)
        {
            if (Instance != null)
            {
                var itemId = Instance.ItemDictionary[itemName];
                RuntimeID runtimeID = itemId;
                ItemObjective itemObjective = new ItemObjective(runtimeID);
                itemObjective.ItemName = itemName;
                itemObjective.QuestName = Instance.QuestDictionary[itemName];
                itemObjective.QuestEntryArgs.entryNumber = Instance.QuestEntryDictionary[itemName];
                Instance.AddItemObjective(itemObjective);
            }
            else
            {
                Debug.LogError("Freehold Drop box inventory instance is not set.");
            }
        }

        public void SetQuestEntrySuccess(string questName, int entryNumber)
        {
            // Check if the quest exists
            if (QuestLog.IsQuestActive(questName))
            {
                // Set the specific entry to success
                QuestLog.SetQuestEntryState(questName, entryNumber, QuestState.Success);
                Debug.Log($"Quest '{questName}' entry {entryNumber} set to success.");
            }
            else
            {
                Debug.LogWarning($"Quest '{questName}' is not active or does not exist.");
            }
        }


        public void OnChangeToDropBoxSlot()
        {
            Debug.Log("Drop box slot changed.");
            foreach (var itemObjective in ItemObjectives)
            {
                foreach (var dropBoxSlot in inventorySlots)
                {
                    var dropBoxItem = dropBoxSlot.GetComponent<UGUITransferableItemSlot>().Peek();

                    if (dropBoxItem.ID == itemObjective.ItemId && dropBoxItem.Value >= itemObjective.ItemQuantity)
                    {
                        Debug.Log(
                            $"Matching item found in dropbox slot: {dropBoxItem.ID}. Objective ID: {itemObjective.ItemId}");

                        SetQuestEntrySuccess(
                            QuestDictionary[itemObjective.ItemName], itemObjective.QuestEntryArgs.entryNumber);

                        QuestManager.Instance.SetEntryToSuccess(
                            QuestDictionary[itemObjective.ItemName], itemObjective.QuestEntryArgs.entryNumber);

                        QuestManager.Instance.CheckAndActivateNextEntries(QuestDictionary[itemObjective.ItemName]);
                    }
                }
            }
        }


        // Update is called once per frame
        void Update()
        {
        }

        void OnDestroy()
        {
            // Unregister the method when this object is destroyed
            Lua.UnregisterFunction("AddItemObjective");
        }
    }
}
