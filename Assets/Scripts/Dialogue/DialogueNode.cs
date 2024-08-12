using System.Collections.Generic;
using System;

namespace Dialogue
{
    [System.Serializable]
    public class DialogueNode
    {
        public string id;
        public string text;
        public Speaker speaker;
        public List<string> choices;
        public List<string> nextNodeIds;
        public Action<DialogueNode> OnNodeEnter; // New field for the action to be performed when entering the node

        public DialogueNode(string id, string text, Speaker speaker, List<string> choices, List<string> nextNodeIds)
        {
            this.id = id;
            this.text = text;
            this.speaker = speaker;
            this.choices = choices;
            this.nextNodeIds = nextNodeIds;
            this.OnNodeEnter = null; // Initialize the action to null
        }

        public DialogueNode()
        {
        }

        public void StartDialogue()
        {
            DialogueManager.StartDialog(this.id);
        }

        // New method to invoke the OnNodeEnter action
        public void EnterNode()
        {
            OnNodeEnter?.Invoke(this);
        }
    }
}
