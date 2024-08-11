using System.Collections.Generic;
using Dialogue;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Polyperfect.Crafting.Demo;
using UnityEngine.Serialization;

public class RevisedInteractiveMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public Text objectNameText;
    public Button infoButton;
    [FormerlySerializedAs("exchangeItems")] [FormerlySerializedAs("actionButton")]
    public Button exchangeItemsButton;
    public Button dialogueButton;
    public float interactableDistance = 30f;
    public GameObject player;

    public BaseInteractable interactable;

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
        exchangeItemsButton.onClick.AddListener(ExchangeItems);
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
        // if distance is less than interactable distance
        var distance = Vector3.Distance(obj.transform.position, player.transform.position);
        if (distance > interactableDistance)
        {
            Debug.Log("Object is too far away!");
            return;
        }
        else
        {
            selectedObject = obj;
            menuPanel.SetActive(true);
            objectNameText.text = obj.objectName;
        }
    }

    public void DeselectObject()
    {
        selectedObject = null;
        menuPanel.SetActive(false);
        interactable.EndInteract(gameObject);
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

    void ExchangeItems()
    {
        if (selectedObject != null)
        {
            interactable.BeginInteract(gameObject);
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
