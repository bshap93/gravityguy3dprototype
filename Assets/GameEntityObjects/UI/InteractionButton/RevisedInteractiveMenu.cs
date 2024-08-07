using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PixelCrushers.DialogueSystem;

public class RevisedInteractiveMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public Text objectNameText;
    public Button infoButton;
    public Button actionButton;
    public Button dialogueButton;

    private Camera mainCamera;
    private PhysicsRaycaster physicsRaycaster;
    private Dialoggable selectedObject;
    private Dialoggable hoveredObject;

    void Start()
    {
        mainCamera = Camera.main;
        physicsRaycaster = mainCamera.GetComponent<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            Debug.LogError("PhysicsRaycaster not found on the main camera!");
        }

        menuPanel.SetActive(false);

        infoButton.onClick.AddListener(ShowInfo);
        actionButton.onClick.AddListener(PerformAction);
        dialogueButton.onClick.AddListener(StartDialogue);
    }


    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            UnhighlightObject();
            return;
        }

        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        Dialoggable interactable = null;
        foreach (RaycastResult result in results)
        {
            interactable = result.gameObject.GetComponent<Dialoggable>();
            if (interactable != null)
                break;
        }

        if (interactable != null)
        {
            if (hoveredObject != interactable)
            {
                UnhighlightObject();
                HighlightObject(interactable);
            }

            if (Input.GetMouseButtonDown(0)) // Left mouse button
            {
                SelectObject(interactable);
            }
        }
        else
        {
            UnhighlightObject();
            if (Input.GetMouseButtonDown(0)) // Left mouse button
            {
                DeselectObject();
            }
        }
    }

    void HighlightObject(Dialoggable obj)
    {
        hoveredObject = obj;
        obj.Highlight();
    }

    void UnhighlightObject()
    {
        if (hoveredObject != null)
        {
            hoveredObject.Unhighlight();
            hoveredObject = null;
        }
    }

    public void SelectObject(Dialoggable obj)
    {
        selectedObject = obj;
        menuPanel.SetActive(true);
        objectNameText.text = obj.objectName;
    }

    public void DeselectObject()
    {
        selectedObject = null;
        menuPanel.SetActive(false);
    }

    public void ToggleSelectedObject(Dialoggable obj)
    {
        if (selectedObject != null)
        {
            DeselectObject();
        }
        else
        {
            SelectObject(obj);
        }
    }

    void ShowInfo()
    {
        if (selectedObject != null)
        {
            Debug.Log($"Showing info for {selectedObject.objectName}");
            // Implement info display logic here
        }
    }

    void PerformAction()
    {
        if (selectedObject != null)
        {
            Debug.Log($"Performing action on {selectedObject.objectName}");
            // Implement action logic here
        }
    }

    void StartDialogue()
    {
        if (selectedObject != null)
        {
            Debug.Log($"Starting dialogue with {selectedObject.objectName}");
            DialogueManager.StartConversation(selectedObject.conversationName);
        }
    }
}
