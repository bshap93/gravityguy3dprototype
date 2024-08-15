using System.Collections.Generic;
using UnityEngine;

namespace Dialogue.DialogueConversations
{
    public class DialogueQuarterMaster : MonoBehaviour
    {
        public DialogueUI dialogueUI;

        void Start()
        {
            SetupTestDialogue();
        }

        void SetupTestDialogue()
        {
            var playerName = DialogueManager.Instance.playerName;
            var starshipName = DialogueManager.Instance.starshipName;
            // Create test dialogue nodes
            CreateAndAddNode(
                "start_2",
                $"Good to see you, {playerName}. Have you brought us the farming supplies we requested?",
                new List<string>
                {
                    "I have brought them.",
                    "Not yet, I'm pretty busy, you know...",
                    "Can we talk about something else?"
                },
                new List<string>
                    { "brought_supplies", "not_brought_supplies", "talk_about_something_else" });


            CreateAndAddNode(
                "brought_supplies",
                "Good, good. Let's see here...",
                new List<string> { "...Wait while he checks our cargo hold..." },
                new List<string> { "wait_while_quartermaster_checks" },
                (node) => { DialogueManager.Instance.AddQuestFromDialogue("2"); });


            CreateAndAddNode(
                "not_brought_supplies",
                "Ok, but we need those supplies as soon as possible. Please hurry.",
                new List<string> { "Disconnect" },
                new List<string> { "end_conversation_quartermaster" });


            CreateAndAddNode(
                "end_conversation_quartermaster",
                "OUOIUOIUOIU",
                new List<string> { },
                new List<string> { });


            CreateAndAddNode(
                "about_captain",
                "Captain Chen is a veteran of three deep space missions. She was chosen for her leadership skills and her ability to make tough decisions under pressure.",
                new List<string> { "She sounds impressive", "Go back" },
                new List<string> { "command_structure", "start" });

            CreateAndAddNode(
                "sendoff",
                "Excellent, Officer. Pick up those supplies and head over right away. And stay safe out there.",
                new List<string> { "Ok, sir" },
                new List<string> { "end_conversation" },
                (node) => { DialogueManager.Instance.AddQuestFromDialogue("1"); });

            CreateAndAddNode(
                "end_conversation",
                "End of conversation",
                new List<string>(),
                new List<string>());
        }

        void SetupFreeholdQuartermasterDialogue()
        {
        }


        void CreateAndAddNode(string id, string text, List<string> choices, List<string> nextNodeIds,
            System.Action<DialogueNode> onNodeEnter = null)
        {
            DialogueNode node = new DialogueNode
            {
                id = id,
                text = text,
                choices = choices,
                nextNodeIds = nextNodeIds,
                OnNodeEnter = onNodeEnter // Assign the action to the OnNodeEnter field
            };

            DialogueManager.Instance.AddDialogueNode(node);
        }
    }
}
