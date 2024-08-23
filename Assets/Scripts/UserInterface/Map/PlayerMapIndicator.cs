using UnityEngine;
using UnityEngine.UI;

public class PlayerMapIndicator : MonoBehaviour
{
    public Transform player;
    public RectTransform mapRectTransform;
    public RectTransform indicatorRectTransform;
    public UnityEngine.Camera mapCamera;

    void Update()
    {
        Vector3 viewportPoint = mapCamera.WorldToViewportPoint(player.position);
        Vector2 worldObjectScreenPosition = new Vector2(
            ((viewportPoint.x * mapRectTransform.sizeDelta.x) - (mapRectTransform.sizeDelta.x * 0.5f)),
            ((viewportPoint.y * mapRectTransform.sizeDelta.y) - (mapRectTransform.sizeDelta.y * 0.5f))
        );

        indicatorRectTransform.anchoredPosition = worldObjectScreenPosition;
    }
}
