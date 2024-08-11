using System.Collections.Generic;

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

        public DialogueNode(string id, string text, Speaker speaker, List<string> choices, List<string> nextNodeIds)
        {
            this.id = id;
            this.text = text;
            this.speaker = speaker;
            this.choices = choices;
            this.nextNodeIds = nextNodeIds;
        }
        public DialogueNode()
        {
        }

        public void StartDialogue()
        {
            DialogueManager.StartDialog(this.id);
        }
    }
}
