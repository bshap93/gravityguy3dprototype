using System.Collections.Generic;
using UnityEngine;

namespace Dialogue.DialogueConversations
{
    public class DialogueWelcome : MonoBehaviour
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
                "start",
                $"Good day, {playerName}. I need you to collect this load of Nitrogen pellets and the soil printer and head over to the Freehold.",
                new List<string>
                {
                    "What are our mission objectives?",
                    "Why can't the AI skiff do it?",
                    "Will do, sir."
                },
                new List<string>
                    { "mission_objectives", "ai_issue", "end_conversation" });


            CreateAndAddNode(
                "ai_issue",
                "The AI skiff is currently undergoing maintenance. It's a routine checkup, but we need someone to deliver the supplies to the Freehold right away.",
                new List<string> { "I understand", "Go back", "Tell me more about the AI" },
                new List<string> { "end_conversation", "start", "ship_ai" }
            );


            CreateAndAddNode(
                "ship_ai",
                "The Neural Network AI is the brain of our ship. It manages life support, navigation, and helps with decision making. It's not sentient, but it's incredibly advanced.",
                new List<string> { "Go back" },
                new List<string> { "start" });


            CreateAndAddNode(
                "mission_objectives",
                "Pick up the load of Nitrogen pellets and the soil printer from the cargo bay and deliver them to the Freehold. The Freehold quartermaster is expecting you.",
                new List<string> { "Go back" },
                new List<string> { "start" });


            CreateAndAddNode(
                "about_captain",
                "Captain Chen is a veteran of three deep space missions. She was chosen for her leadership skills and her ability to make tough decisions under pressure.",
                new List<string> { "She sounds impressive", "Go back" },
                new List<string> { "command_structure", "start" });

            CreateAndAddNode(
                "end_conversation",
                "Excellent, Officer. Pick up those supplies and head over right away. And stay safe out there.",
                new List<string>(),
                new List<string>(),
                (node) => { DialogueManager.Instance.AddQuestFromDialogue("1"); });
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
