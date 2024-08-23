using System.Collections.Generic;
using CameraScripts.Minimap;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Map
{
    public class MainMapEnhancer : MonoBehaviour
    {
        public Camera mapCamera;
        public RectTransform mapRect;
        public GameObject poiIconPrefab;
        public float iconScale = 1f;
        public float nameOffsetY = 20f;
        public TMP_FontAsset nameFont;
        public int nameFontSize = 12;
        public Color nameColor = Color.white;

        public Dictionary<PointOfInterest, (Image icon, TextMeshProUGUI nameText)> poiElements =
            new Dictionary<PointOfInterest, (Image, TextMeshProUGUI)>();

        private void Start()
        {
            FindAllPOIs();
        }

        private void Update()
        {
            UpdateMapIcons();
        }

        private void FindAllPOIs()
        {
            PointOfInterest[] pois = FindObjectsOfType<PointOfInterest>();
            foreach (var poi in pois)
            {
                CreateMapIcon(poi);
            }
        }

        private void CreateMapIcon(PointOfInterest poi)
        {
            GameObject iconObject = Instantiate(poiIconPrefab, mapRect);
            Image iconImage = iconObject.GetComponent<Image>();
            iconImage.sprite = poi.minimapIcon;
            iconImage.rectTransform.sizeDelta *= iconScale;

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

        private void UpdateMapIcons()
        {
            foreach (var kvp in poiElements)
            {
                PointOfInterest poi = kvp.Key;
                (Image iconImage, TextMeshProUGUI nameText) = kvp.Value;

                Vector2 screenPoint = mapCamera.WorldToViewportPoint(poi.transform.position);
                iconImage.rectTransform.anchorMin = screenPoint;
                iconImage.rectTransform.anchorMax = screenPoint;
                iconImage.rectTransform.anchoredPosition = Vector2.zero;

                bool isVisible = screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
                iconImage.enabled = isVisible;
                nameText.enabled = isVisible;
            }
        }

        public void AddPOI(PointOfInterest newPOI)
        {
            if (!poiElements.ContainsKey(newPOI))
            {
                CreateMapIcon(newPOI);
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
