using UnityEngine;
using UnityEngine.EventSystems;

public class MinMap : MonoBehaviour, IPointerClickHandler
{
    public Camera mainCamera;

    public float minX = -2f;
    public float maxX = 2f;
    public float minZ = -2f;
    public float maxZ = 2f; // Optional: can use for vertical clicks

    private RectTransform minimapRect;

    void Start()
    {
        minimapRect = GetComponent<RectTransform>();

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localPoint;

        // Convert screen point to local point inside the mini-map
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            minimapRect,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        {
            // Convert localPoint (which is relative to the rect's pivot) to 0..1 based on rect bounds.
            // This method works regardless of the RectTransform's pivot/anchor.
            Rect r = minimapRect.rect;
            float normalizedX = (localPoint.x - r.xMin) / r.width;
            float normalizedZ = (localPoint.y - r.yMin) / r.height;

            normalizedX = Mathf.Clamp01(normalizedX);
            normalizedZ = Mathf.Clamp01(normalizedZ);

            float worldX = Mathf.Lerp(minX, maxX, normalizedX);
            // Always set z to -2
            float worldZ = -2f;

            // Set camera position (Y stays the same)
            mainCamera.transform.position = new Vector3(worldX, mainCamera.transform.position.y, worldZ);

            Debug.Log($"CLICK → normalizedX: {normalizedX} worldX: {worldX}");
        }
    }
}
