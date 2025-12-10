using UnityEngine;

public class SimpleZoom : MonoBehaviour
{
    public float zoomSpeed = 10f;
    public float minDistance = 5f;
    public float maxDistance = 50f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            // Move camera forward or backward
            transform.position += transform.forward * scroll * zoomSpeed;
            
            // Clamp distance
            float distance = Vector3.Distance(transform.position, Vector3.zero);
            distance = Mathf.Clamp(distance, minDistance, maxDistance);

            // Adjust position if out of limits
            transform.position = transform.position.normalized * distance;
        }
    }
}
