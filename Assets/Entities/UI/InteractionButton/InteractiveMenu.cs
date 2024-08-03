using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using PixelCrushers.DialogueSystem;

public class InteractiveMenu : MonoBehaviour
{
    public GameObject menuPanel;
    public Text objectNameText;
    public Button infoButton;
    public Button actionButton;
    public Button dialogueButton;
    public float interactionDistance = 100f;

    private Camera mainCamera;
    private InteractableObject selectedObject;
    private InteractableObject hoveredObject;

    void Start()
    {
        mainCamera = Camera.main;
        menuPanel.SetActive(false);

        infoButton.onClick.AddListener(ShowInfo);
        actionButton.onClick.AddListener(PerformAction);
        dialogueButton.onClick.AddListener(StartDialogue);
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Mouse is over UI, unhighlight any highlighted object
            UnhighlightObject();
            return;
        }

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
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
        else
        {
            UnhighlightObject();
            if (Input.GetMouseButtonDown(0)) // Left mouse button
            {
                DeselectObject();
            }
        }
    }

    void HighlightObject(InteractableObject obj)
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

    void SelectObject(InteractableObject obj)
    {
        selectedObject = obj;
        menuPanel.SetActive(true);
        objectNameText.text = obj.objectName;
    }

    void DeselectObject()
    {
        selectedObject = null;
        menuPanel.SetActive(false);
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
