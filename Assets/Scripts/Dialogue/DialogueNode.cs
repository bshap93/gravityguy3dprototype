using System.Collections.Generic;

namespace Dialogue
{
    [System.Serializable]
    public class DialogueNode
    {
        public string id;
        public string text;
        public List<string> choices;
        public List<string> nextNodeIds;
    }
}
