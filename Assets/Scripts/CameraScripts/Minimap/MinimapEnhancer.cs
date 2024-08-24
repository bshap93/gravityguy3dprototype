﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UserInterface.Map;

namespace CameraScripts.Minimap
{
    public class MinimapEnhancer : MonoBehaviour
    {
        public Camera minimapCamera;
        public RectTransform minimapRect;
        public GameObject poiIconPrefab;
        public float iconScale = 1f;
        public float nameOffsetY = 20f; // Offset for the name text
        public TMP_FontAsset nameFont;
        public int nameFontSize = 12;
        public Color nameColor = Color.white;
        public GameObject minimapPanel;

        private Dictionary<PointOfInterest, (Image icon, TextMeshProUGUI nameText)> poiElements =
            new Dictionary<PointOfInterest, (Image, TextMeshProUGUI)>();

        public FullMapController fullMapController;

        private void OnEnable()
        {
            // Subscribe to the event
            if (fullMapController != null)
            {
                fullMapController.onFullMapOpenEvent.AddListener(CloseMiniMap);
                fullMapController.onFullMapCloseEvent.AddListener(OpenMiniMap);
            }
        }

        private void OnDisable()
        {
            // Unsubscribe from the event
            if (fullMapController != null)
            {
                fullMapController.onFullMapOpenEvent.RemoveListener(ToggleMinimap);
                fullMapController.onFullMapCloseEvent.RemoveListener(ToggleMinimap);
            }
        }

        private void Start()
        {
            FindAllPOIs();
        }

        private void Update()
        {
            UpdateMinimapIcons();
        }

        private void FindAllPOIs()
        {
            PointOfInterest[] pois = FindObjectsOfType<PointOfInterest>();
            foreach (var poi in pois)
            {
                CreateMinimapIcon(poi);
            }
        }

        void ToggleMinimap()
        {
            // Toggle minimap visibility

            minimapPanel.gameObject.SetActive(!minimapRect.gameObject.activeSelf);
        }

        void OpenMiniMap()
        {
            minimapPanel.gameObject.SetActive(true);
        }

        void CloseMiniMap()
        {
            minimapPanel.gameObject.SetActive(false);
        }

        private void CreateMinimapIcon(PointOfInterest poi)
        {
            GameObject iconObject = Instantiate(poiIconPrefab, minimapRect);
            Image iconImage = iconObject.GetComponent<Image>();
            iconImage.sprite = poi.minimapIcon;
            iconImage.rectTransform.sizeDelta *= iconScale;

            // Create name text
            GameObject nameObject = new GameObject("POI Name");
            nameObject.transform.SetParent(iconObject.transform, false);
            TextMeshProUGUI nameText = nameObject.AddComponent<TextMeshProUGUI>();
            nameText.text = string.IsNullOrEmpty(poi.poiName) ? poi.gameObject.name : poi.poiName;
            nameText.font = nameFont;
            nameText.fontSize = nameFontSize;
            nameText.color = nameColor;
            nameText.alignment = TextAlignmentOptions.Center;
            nameText.rectTransform.anchoredPosition = new Vector2(0, nameOffsetY);

            poiElements[poi] = (iconImage, nameText);
        }

        private void UpdateMinimapIcons()
        {
            foreach (var kvp in poiElements)
            {
                PointOfInterest poi = kvp.Key;
                (Image iconImage, TextMeshProUGUI nameText) = kvp.Value;

                Vector2 screenPoint = minimapCamera.WorldToViewportPoint(poi.transform.position);
                iconImage.rectTransform.anchorMin = screenPoint;
                iconImage.rectTransform.anchorMax = screenPoint;
                iconImage.rectTransform.anchoredPosition = Vector2.zero;

                // Hide icons and names that are outside the minimap view
                bool isVisible = screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
                iconImage.enabled = isVisible;
                nameText.enabled = isVisible;
            }
        }

        public void AddPOI(PointOfInterest newPOI)
        {
            if (!poiElements.ContainsKey(newPOI))
            {
                CreateMinimapIcon(newPOI);
            }
        }

        public void RemovePOI(PointOfInterest poiToRemove)
        {
            if (poiElements.TryGetValue(poiToRemove, out var elements))
            {
                Destroy(elements.icon.gameObject);
                poiElements.Remove(poiToRemove);
            }
        }
    }
}