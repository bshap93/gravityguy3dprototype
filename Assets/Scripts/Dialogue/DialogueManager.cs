using System;
using System.Collections.Generic;
using Quests;
using UnityEngine;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }
        private QuestManager _questManager;

        public string playerName;
        public string starshipName;

        public DialogueUI dialogueUi;

        Dictionary<string, DialogueNode> _dialogueNodes = new Dictionary<string, DialogueNode>();

        private void Start()
        {
            _questManager = QuestManager.Instance;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddQuestFromDialogue(string questId)
        {
            var quest = _questManager.GetQuest(questId);
            _questManager.AddQuest(questId);
        }


        public void ClearDialogueNodes()
        {
            _dialogueNodes.Clear();
        }

        public void AddDialogueNode(DialogueNode dialogueNode, Action<DialogueNode> onEnterAction = null)
        {
            if (!_dialogueNodes.ContainsKey(dialogueNode.id))
            {
                _dialogueNodes.Add(dialogueNode.id, dialogueNode);
                if (onEnterAction != null)
                {
                    dialogueNode.OnNodeEnter += onEnterAction;
                }
            }
            else
            {
                Debug.LogWarning($"Dialogue node with id {dialogueNode.id} already exists. Skipping addition.");
            }
        }

        public static void StartDialog(string dialogueNodeId)
        {
            if (Instance == null)
            {
                Debug.LogError("DialogueManager instance is null. Ensure it's created in the scene.");
                return;
            }

            if (Instance._dialogueNodes.TryGetValue(dialogueNodeId, out DialogueNode node))
            {
                node.EnterNode();
                node.StartDialogue();
            }
            else
            {
                Debug.LogError($"No dialogue node found with id: {dialogueNodeId}");
            }
        }

        public DialogueNode GetDialogueNode(string startNodeId)
        {
            if (_dialogueNodes.TryGetValue(startNodeId, out DialogueNode node))
            {
                node.OnNodeEnter?.Invoke(node);
                return node;
            }

            Debug.LogError($"No dialogue node found with id: {startNodeId}");
            return null;
        }


        private void OnDestroy()
        {
            foreach (var node in _dialogueNodes.Values)
            {
                node.OnNodeEnter = null;
            }

            _dialogueNodes.Clear();
            Instance = null; // Ensure the static instance is also cleared
        }
    }
}
