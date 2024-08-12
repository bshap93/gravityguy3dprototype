using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public Transform target;
    public Transform player;
    public RectTransform arrow;
    public float edgeBuffer = 100f;

    private Camera mainCamera;
    private RectTransform canvasRect;

    void Start()
    {
        mainCamera = Camera.main;
        canvasRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (target == null || player == null || arrow == null) return;

        Vector3 directionToTarget = target.position - player.position;
        directionToTarget.y = 0; // Ignore Y axis

        Vector3 targetScreenPosition = mainCamera.WorldToScreenPoint(target.position);

        if (targetScreenPosition.z > 0 &&
            targetScreenPosition.x > 0 && targetScreenPosition.x < Screen.width &&
            targetScreenPosition.y > 0 && targetScreenPosition.y < Screen.height)
        {
            // Target is on screen
            arrow.gameObject.SetActive(false);
        }
        else
        {
            // Target is off screen
            arrow.gameObject.SetActive(true);

            targetScreenPosition.x = Mathf.Clamp(targetScreenPosition.x, edgeBuffer, Screen.width - edgeBuffer);
            targetScreenPosition.y = Mathf.Clamp(targetScreenPosition.y, edgeBuffer, Screen.height - edgeBuffer);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect, targetScreenPosition, null, out Vector2 localPoint);

            arrow.localPosition = localPoint;

            float angle = Mathf.Atan2(directionToTarget.z, directionToTarget.x) * Mathf.Rad2Deg;
            arrow.localRotation = Quaternion.Euler(0, 0, -angle + 90);
        }
    }
}
