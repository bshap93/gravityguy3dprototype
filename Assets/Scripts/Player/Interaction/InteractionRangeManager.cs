using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Player.Interaction
{
    public class InteractionRangeManager : MonoBehaviour
    {
        public static InteractionRangeManager Instance { get; private set; }

        public Transform player;
        public float checkInterval = 1f; // Check every second

        private Dictionary<MyInteractable, bool> interactablesInRange = new Dictionary<MyInteractable, bool>();

        // UnityEvent that fires when player enters interaction range
        [System.Serializable]
        public class InteractionRangeEnteredEvent : UnityEvent<MyInteractable>
        {
        }

        [FormerlySerializedAs("OnInteractionRangeEntered")]
        public InteractionRangeEnteredEvent onInteractionRangeEntered;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            // Initialize the UnityEvent
            if (onInteractionRangeEntered == null)
                onInteractionRangeEntered = new InteractionRangeEnteredEvent();
        }

        private void Start()
        {
            StartCoroutine(CheckInteractionRanges());
        }

        public void RegisterInteractable(MyInteractable interactable)
        {
            if (!interactablesInRange.ContainsKey(interactable))
            {
                interactablesInRange.Add(interactable, false);
            }
        }

        public void UnregisterInteractable(MyInteractable interactable)
        {
            if (interactablesInRange.ContainsKey(interactable))
            {
                interactablesInRange.Remove(interactable);
            }
        }

        private IEnumerator CheckInteractionRanges()
        {
            while (true)
            {
                foreach (var kvp in interactablesInRange)
                {
                    MyInteractable interactable = kvp.Key;
                    bool wasInRange = kvp.Value;

                    bool isInRange = DistanceUtility.IsWithinInteractionDistance(
                        player,
                        interactable.boxCollider.transform,
                        interactable.interactableDistance
                    );

                    if (isInRange && !wasInRange)
                    {
                        interactablesInRange[interactable] = true;
                        onInteractionRangeEntered.Invoke(interactable);
                    }
                    else if (!isInRange && wasInRange)
                    {
                        interactablesInRange[interactable] = false;
                    }
                }

                yield return new WaitForSeconds(checkInterval);
            }
        }
    }
}
