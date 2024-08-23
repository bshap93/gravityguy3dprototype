using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UserInterface.Map
{
    public class FullMapController : MonoBehaviour
    {
        public UnityEvent onFullMapOpenEvent;
        public UnityEvent onFullMapCloseEvent;

        [FormerlySerializedAs("_isFullMapOpen")] [SerializeField]
        private bool isFullMapOpen = false;


        public UnityEngine.Camera mapCamera;
        public RawImage mapImage;
        public Image gridImage;
        public float zoomSpeed = 1f;
        public float minZoom = 5f;
        public float maxZoom = 20f;

        private bool _isMapVisible = false;

        private MainMapEnhancer _mapEnhancer;

        void Start()
        {
            if (onFullMapOpenEvent == null)
                onFullMapOpenEvent = new UnityEvent();

            if (onFullMapCloseEvent == null)
                onFullMapCloseEvent = new UnityEvent();

            _mapEnhancer = GetComponent<MainMapEnhancer>();
            if (_mapEnhancer == null)
            {
                Debug.LogError("MainMapEnhancer component not found!");
            }

            _isMapVisible = false;
            mapImage.gameObject.SetActive(false);
            gridImage.gameObject.SetActive(false);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleMap();
            }

            if (_isMapVisible)
            {
                HandleZoom();
            }
        }

        void ToggleMap()
        {
            if (isFullMapOpen)
            {
                CloseFullMap();
            }
            else
            {
                OpenFullMap();
            }
            // _isMapVisible = !_isMapVisible;
            // mapImage.gameObject.SetActive(_isMapVisible);
            // gridImage.gameObject.SetActive(_isMapVisible);
            // minimapToggleActive.Invoke();
        }

        private void OpenFullMap()
        {
            _isMapVisible = true;
            mapImage.gameObject.SetActive(true);
            gridImage.gameObject.SetActive(true);
            isFullMapOpen = true;
            onFullMapOpenEvent.Invoke();

            foreach (var poiElement in _mapEnhancer.poiElements.Values)
            {
                poiElement.icon.gameObject.SetActive(true);
            }
        }

        private void CloseFullMap()
        {
            _isMapVisible = false;
            mapImage.gameObject.SetActive(false);
            gridImage.gameObject.SetActive(false);
            isFullMapOpen = false;
            onFullMapCloseEvent.Invoke();

            foreach (var poiElement in _mapEnhancer.poiElements.Values)
            {
                poiElement.icon.gameObject.SetActive(false);
            }
        }

        void HandleZoom()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                float newSize = mapCamera.orthographicSize - scroll * zoomSpeed;
                mapCamera.orthographicSize = Mathf.Clamp(newSize, minZoom, maxZoom);
            }
        }
    }
}
