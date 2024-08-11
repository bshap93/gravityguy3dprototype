using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }

        public DialogueUI dialogueUi;

        Dictionary<string, DialogueNode> _dialogueNodes = new Dictionary<string, DialogueNode>();

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

        public void ClearDialogueNodes()
        {
            _dialogueNodes.Clear();
        }

        public void AddDialogueNode(DialogueNode dialogueNode)
        {
            if (!_dialogueNodes.ContainsKey(dialogueNode.id))
            {
                _dialogueNodes.Add(dialogueNode.id, dialogueNode);
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
                return node;
            }

            Debug.LogError($"No dialogue node found with id: {startNodeId}");
            return null;
        }
    }
}
