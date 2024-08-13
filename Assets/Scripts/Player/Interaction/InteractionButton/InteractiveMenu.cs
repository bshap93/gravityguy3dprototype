using System.Collections.Generic;
using Player.Interaction.InteractionButton;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractiveMenu : MonoBehaviour
{
    [Header("UI Elements")] [SerializeField]
    private GameObject menuPanel;
    [SerializeField] private Text objectNameText;
    [SerializeField] private Button infoButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button dialogueButton;

    [Header("Interaction Settings")] [SerializeField]
    private float interactableDistance = 5f;
    [SerializeField] private LayerMask interactableLayer;

    [Header("References")] [SerializeField]
    private GameObject player;

    private Camera mainCamera;
    private IInteractable currentInteractable;

    private void Start()
    {
        mainCamera = Camera.main;
        menuPanel.SetActive(false);

        infoButton.onClick.AddListener(ShowInfo);
        interactButton.onClick.AddListener(Interact);
        dialogueButton.onClick.AddListener(StartDialogue);
    }

    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        HandleInteractableDetection();
        HandleInteraction();
    }

    private void HandleInteractableDetection()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactableDistance, interactableLayer))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (interactable != currentInteractable)
                {
                    if (currentInteractable != null)
                    {
                        currentInteractable.OnHoverExit();
                    }

                    currentInteractable = interactable;
                    currentInteractable.OnHoverEnter();
                }
            }
            else
            {
                ClearCurrentInteractable();
            }
        }
        else
        {
            ClearCurrentInteractable();
        }
    }

    private void ClearCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable.OnHoverExit();
            currentInteractable = null;
        }
    }

    private void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0) && currentInteractable != null)
        {
            float distance = Vector3.Distance(currentInteractable.GetPosition(), player.transform.position);
            if (distance <= interactableDistance)
            {
                ShowMenu(currentInteractable);
            }
            else
            {
                Debug.Log($"Object is too far away. Distance: {distance}");
            }
        }
        else if (Input.GetMouseButtonDown(0) && menuPanel.activeSelf)
        {
            HideMenu();
        }
    }

    private void ShowMenu(IInteractable interactable)
    {
        menuPanel.SetActive(true);
        objectNameText.text = interactable.GetName();

        // Enable/disable buttons based on interactable capabilities
        infoButton.gameObject.SetActive(interactable.HasInfo());
        interactButton.gameObject.SetActive(interactable.CanInteract());
        dialogueButton.gameObject.SetActive(interactable.HasDialogue());
    }

    private void HideMenu()
    {
        menuPanel.SetActive(false);
        if (currentInteractable != null)
        {
            currentInteractable.OnInteractionEnd();
        }
    }

    private void ShowInfo()
    {
        if (currentInteractable != null)
        {
            currentInteractable.ShowInfo();
        }
    }

    private void Interact()
    {
        if (currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void StartDialogue()
    {
        if (currentInteractable != null)
        {
            currentInteractable.StartDialogue();
        }
    }
}
