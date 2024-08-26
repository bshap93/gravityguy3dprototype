using PixelCrushers.DialogueSystem;
using Player.Interaction;
using UnityEngine;
using UnityEngine.Serialization;

namespace Items
{
    public class InteractableRangeEntered : MonoBehaviour
    {
        [SerializeField] public string QuestName;
        public int QuestEntryNumber;
        public QuestState NewQuestState;
    }
}
