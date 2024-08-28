using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class MainMapCameraController : MonoBehaviour
{
    [SerializeField] private Camera mainMapCamera;
    [FormerlySerializedAs("_currentZoomLevel")] [SerializeField]
    ZoomLevel currentZoomLevel = ZoomLevel.Medium;

    public enum ZoomLevel
    {
        Close,
        Medium,
        Far
    }

    // Start is called before the first frame update
    void Start()
    {
        mainMapCamera = GetComponent<Camera>();
    }


    public void MoveCamera(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    void AdjustCameraZoom(ZoomLevel zoomLevel)
    {
        switch (zoomLevel)
        {
            case ZoomLevel.Close:
                mainMapCamera.orthographicSize = 1328;
                currentZoomLevel = ZoomLevel.Close;
                break;
            case ZoomLevel.Medium:
                mainMapCamera.orthographicSize = 11704;
                currentZoomLevel = ZoomLevel.Medium;
                break;
            case ZoomLevel.Far:
                mainMapCamera.orthographicSize = 23408;
                currentZoomLevel = ZoomLevel.Far;
                break;
        }
    }

    public void IncreaseCameraZoom()
    {
        switch (currentZoomLevel)
        {
            case ZoomLevel.Close:
                AdjustCameraZoom(ZoomLevel.Medium);
                break;
            case ZoomLevel.Medium:
                AdjustCameraZoom(ZoomLevel.Far);
                break;
            case ZoomLevel.Far:
                Debug.Log("Already at maximum zoom level");
                break;
        }
    }

    public void DecreaseCameraZoom()
    {
        switch (currentZoomLevel)
        {
            case ZoomLevel.Close:
                Debug.Log("Already at minimum zoom level");
                break;
            case ZoomLevel.Medium:
                AdjustCameraZoom(ZoomLevel.Close);
                break;
            case ZoomLevel.Far:
                AdjustCameraZoom(ZoomLevel.Medium);
                break;
        }
    }
}
