using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public static object Instance { get; set; }

        Dictionary<string, DialogueNode> _dialogueNodes = new();

        public void AddDialogueNode(DialogueNode dialogueNode)
        {
            _dialogueNodes.Add(dialogueNode.id, dialogueNode);
        }

        public DialogueNode GetDialogueNode(string id)
        {
            return _dialogueNodes.GetValueOrDefault(id);
        }


        public static void StartConversation(string selectedObjectConversationName)
        {
            throw new System.NotImplementedException();
        }
    }
}
