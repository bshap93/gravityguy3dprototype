using UnityEngine;
using UnityEngine.UI;

public class FullMapController : MonoBehaviour
{
    public Camera mapCamera;
    public RawImage mapImage;
    public Image gridImage;
    public float zoomSpeed = 1f;
    public float minZoom = 5f;
    public float maxZoom = 20f;

    private bool isMapVisible = false;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap();
        }

        if (isMapVisible)
        {
            HandleZoom();
        }
    }

    void ToggleMap()
    {
        isMapVisible = !isMapVisible;
        mapImage.gameObject.SetActive(isMapVisible);
        gridImage.gameObject.SetActive(isMapVisible);
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
