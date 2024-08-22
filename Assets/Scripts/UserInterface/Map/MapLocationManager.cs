using UnityEngine;
using UnityEngine.UI;

namespace UserInterface.Map
{
    public class MapLocationManager : MonoBehaviour
    {
        public static MapLocationManager Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
        }
        
        public RectTransform mapRectTransform;
        public GameObject locationNamePrefab;

        // Call this method to add a new location name
        public void AddLocationName(string name, Vector2 position)
        {
            GameObject newLocation = Instantiate(locationNamePrefab, mapRectTransform);
            RectTransform rectTransform = newLocation.GetComponent<RectTransform>();
            rectTransform.anchorMin = rectTransform.anchorMax = new Vector2(position.x, position.y);
            rectTransform.anchoredPosition = Vector2.zero;

            Text textComponent = newLocation.GetComponent<Text>();
            textComponent.text = name;
        }
    }
}
