using UnityEngine;
using Dialogue;
using System.Collections.Generic;

public class DialogueTest : MonoBehaviour
{
    public DialogueUI dialogueUI;

    void Start()
    {
        SetupTestDialogue();
    }

    void Update()
    {
        // Start the conversation when the player presses the Space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            dialogueUI.gameObject.SetActive(true);
            dialogueUI.StartConversation("start");
        }
    }

    void SetupTestDialogue()
    {
        // Create test dialogue nodes
        CreateAndAddNode(
            "start",
            "Welcome to the Starship Horizon, Officer. This is your first day aboard. What would you like to know?",
            new List<string>
            {
                "Tell me about the ship", "What are our mission objectives?", "Who's in charge here?",
                "I'm ready for duty"
            },
            new List<string> { "ship_info", "mission_objectives", "command_structure", "end_conversation" });

        CreateAndAddNode(
            "ship_info",
            "The Starship Horizon is a state-of-the-art generation ship, designed for our long journey to Tau Ceti. It's equipped with advanced life support systems, hydroponic farms, and a fusion reactor.",
            new List<string> { "How long is our journey?", "Tell me more about the systems", "Go back" },
            new List<string> { "journey_length", "ship_systems", "start" });

        CreateAndAddNode(
            "journey_length",
            "Our journey to Tau Ceti is expected to take approximately 200 years. We're traveling at 0.1c, or 10% of the speed of light.",
            new List<string> { "That's a long time!", "Go back" },
            new List<string> { "long_journey", "ship_info" });

        CreateAndAddNode(
            "long_journey",
            "Indeed it is. That's why we're a generation ship. Many generations will live and die before we reach our destination.",
            new List<string> { "I understand", "Go back to main topics" },
            new List<string> { "ship_info", "start" });

        CreateAndAddNode(
            "ship_systems",
            "Our key systems include the Neural Network AI for ship management, the Biodome for food production, and the Cryo-storage facilities for emergency use.",
            new List<string> { "Tell me about the AI", "What's in the Biodome?", "Go back" },
            new List<string> { "ship_ai", "biodome", "ship_info" });

        CreateAndAddNode(
            "ship_ai",
            "The Neural Network AI is the brain of our ship. It manages life support, navigation, and helps with decision making. It's not sentient, but it's incredibly advanced.",
            new List<string> { "Interesting", "Go back" },
            new List<string> { "ship_systems", "start" });

        CreateAndAddNode(
            "biodome",
            "The Biodome is where we grow our food. It's a massive hydroponic and aeroponic facility that ensures we have a sustainable food supply for the entire journey.",
            new List<string> { "Impressive", "Go back" },
            new List<string> { "ship_systems", "start" });

        CreateAndAddNode(
            "mission_objectives",
            "Our primary mission is to establish a human colony in the Tau Ceti system. We carry with us the hopes and dreams of humanity for a new home among the stars.",
            new List<string> { "Why Tau Ceti?", "What challenges do we face?", "Go back" },
            new List<string> { "why_tau_ceti", "mission_challenges", "start" });

        CreateAndAddNode(
            "why_tau_ceti",
            "Tau Ceti was chosen because it's relatively close at 12 light-years away, and it has several potentially habitable planets in its system.",
            new List<string> { "I see", "Go back" },
            new List<string> { "mission_objectives", "start" });

        CreateAndAddNode(
            "mission_challenges",
            "Our main challenges are the length of the journey, maintaining a stable society over generations, and the unknown factors we might encounter at Tau Ceti.",
            new List<string> { "That's daunting", "Go back" },
            new List<string> { "mission_objectives", "start" });

        CreateAndAddNode(
            "command_structure",
            "The ship is led by Captain Amelia Chen. Below her are the department heads: Engineering, Sciences, Medical, and Operations. You'll be reporting to Operations.",
            new List<string> { "Tell me about the Captain", "What does Operations do?", "Go back" },
            new List<string> { "about_captain", "about_operations", "start" });

        CreateAndAddNode(
            "about_captain",
            "Captain Chen is a veteran of three deep space missions. She was chosen for her leadership skills and her ability to make tough decisions under pressure.",
            new List<string> { "She sounds impressive", "Go back" },
            new List<string> { "command_structure", "start" });

        CreateAndAddNode(
            "about_operations",
            "Operations handles the day-to-day running of the ship, including resource management, personnel assignments, and maintaining ship discipline.",
            new List<string> { "I understand", "Go back" },
            new List<string> { "command_structure", "start" });

        CreateAndAddNode(
            "end_conversation",
            "Excellent, Officer. Report to your station in Operations. Remember, the success of our mission depends on each one of us doing our part. Dismissed.",
            new List<string>(),
            new List<string>());
    }

    void CreateAndAddNode(string id, string text, List<string> choices, List<string> nextNodeIds)
    {
        DialogueNode node = new DialogueNode
        {
            id = id,
            text = text,
            choices = choices,
            nextNodeIds = nextNodeIds
        };

        DialogueManager.Instance.AddDialogueNode(node);
    }
}
