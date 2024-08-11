using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;
using Dialogue;

public class DialogueUI : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject optionButtonPrefab;
    public RectTransform optionButtonContainer;

    private DialogueNode currentNode;
    private List<GameObject> currentButtons = new List<GameObject>();

    public void StartConversation(string startNodeId)
    {
        currentNode = DialogueManager.Instance.GetDialogueNode(startNodeId);
        if (currentNode != null)
        {
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

        // Force the layout group to update immediately
        LayoutRebuilder.ForceRebuildLayoutImmediate(optionButtonContainer);

        if (node.choices.Count == 0)
        {
            EndConversation();
        }
    }

    private void MakeChoice(int choiceIndex)
    {
        if (choiceIndex < currentNode.nextNodeIds.Count)
        {
            string nextNodeId = currentNode.nextNodeIds[choiceIndex];
            currentNode = DialogueManager.Instance.GetDialogueNode(nextNodeId);
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
        ClearOptionButtons();
        gameObject.SetActive(false);
    }
}
