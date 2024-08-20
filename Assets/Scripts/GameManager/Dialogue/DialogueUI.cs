using System.Collections.Generic;
using Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameManager.Dialogue
{
    public class DialogueUI : MonoBehaviour
    {
        public TextMeshProUGUI dialogueText;
        public GameObject optionButtonPrefab;
        public RectTransform optionButtonContainer;

        // New audio-related variables
        [Header("Audio")] public AudioSource audioSource;
        public AudioClip startConversationSound;
        public AudioClip displayTextSound;
        public AudioClip selectOptionSound;
        public AudioClip endConversationSound;

        private DialogueNode currentNode;
        private List<GameObject> currentButtons = new List<GameObject>();

        public void StartConversation(string startNodeId)
        {
            currentNode = HomebrewDialogueManager.Instance.GetDialogueNode(startNodeId);
            if (currentNode != null)
            {
                gameObject.SetActive(true);
                PlaySound(startConversationSound);
                DisplayNode(currentNode);
            }
            else
            {
                Debug.LogError($"Start node with ID {startNodeId} not found.");
            }
        }

        private void DisplayNode(DialogueNode node)
        {
            dialogueText.text = node.text;
            PlaySound(displayTextSound);

            ClearOptionButtons();

            for (int i = 0; i < node.choices.Count; i++)
            {
                GameObject buttonObj = Instantiate(optionButtonPrefab, optionButtonContainer);
                Button button = buttonObj.GetComponent<Button>();
                TextMeshProUGUI buttonText = buttonObj.GetComponentInChildren<TextMeshProUGUI>();
                buttonObj.gameObject.transform.position = new Vector3(
                    buttonObj.gameObject.transform.position.x, buttonObj.gameObject.transform.position.y - i * 100, 0);

                buttonText.text = node.choices[i];
                int choiceIndex = i;
                button.onClick.AddListener(() => MakeChoice(choiceIndex));

                currentButtons.Add(buttonObj);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(optionButtonContainer);

            if (node.choices.Count == 0)
            {
                EndConversation();
            }
        }

        private void MakeChoice(int choiceIndex)
        {
            PlaySound(selectOptionSound);

            if (choiceIndex < currentNode.nextNodeIds.Count)
            {
                string nextNodeId = currentNode.nextNodeIds[choiceIndex];
                currentNode = HomebrewDialogueManager.Instance.GetDialogueNode(nextNodeId);
                if (currentNode != null)
                {
                    DisplayNode(currentNode);
                }
                else
                {
                    Debug.LogError($"Next node with ID {nextNodeId} not found.");
                    EndConversation();
                }
            }
            else
            {
                Debug.LogError("Choice index out of range.");
                EndConversation();
            }
        }

        private void ClearOptionButtons()
        {
            foreach (GameObject button in currentButtons)
            {
                Destroy(button);
            }

            currentButtons.Clear();
        }

        private void EndConversation()
        {
            Debug.Log("Conversation ended.");
            PlaySound(endConversationSound);
            ClearOptionButtons();
            gameObject.SetActive(false);
        }

        private void PlaySound(AudioClip clip)
        {
            if (audioSource != null && clip != null)
            {
                audioSource.PlayOneShot(clip);
            }
        }
    }
}
