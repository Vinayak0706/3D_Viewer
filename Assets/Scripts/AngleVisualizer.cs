using UnityEngine;
using TMPro;

public class AngleVisualizer : MonoBehaviour
{
    public LineRenderer lineAB;
    public LineRenderer lineCB;
    public LineRenderer arcRenderer;
    public TextMeshPro angleText;

    private Transform A, B, C;
    int arcResolution = 60;
    bool textPlaced = false;

    public void SetPoints(Transform a, Transform b, Transform c)
    {
        A = a; B = b; C = c;
        textPlaced = false;
        UpdateAngle();
        PlaceAngleTextOnce();
    }

    void Update()
    {
        if (A && B && C)
            UpdateAngle();
    }

    void UpdateAngle()
    {
        Vector3 BA = (A.position - B.position).normalized;
        Vector3 BC = (C.position - B.position).normalized;
        float angle = Vector3.Angle(BA, BC);

        lineAB.positionCount = 2;
        lineAB.SetPosition(0, B.position);
        lineAB.SetPosition(1, A.position);

        lineCB.positionCount = 2;
        lineCB.SetPosition(0, B.position);
        lineCB.SetPosition(1, C.position);

        DrawArc(B.position, BA, angle);

        if (angleText != null)
            angleText.text = angle.ToString("0.0°");
    }

    void PlaceAngleTextOnce()
    {
        if (textPlaced || angleText == null) return;

        // Calculate direction vectors
        Vector3 BA = (A.position - B.position).normalized;
        Vector3 BC = (C.position - B.position).normalized;

        // Bisector between BA and BC (points naturally between lines)
        Vector3 bisector = (BA + BC).normalized;

        // Push slightly outward based on arc radius
        float outwardDistance = 0.15f; // tweakable, bigger = farther from center
        Vector3 basePos = B.position + bisector * outwardDistance;

        // Smooth adjustment so text doesn't sit behind camera
        Vector3 camDir = (Camera.main.transform.position - basePos).normalized;
        basePos += camDir * 0.02f; // tiny forward nudge for visibility

        // Apply final position
        angleText.transform.position = basePos;

        // Face toward camera once (not updating later)
        angleText.transform.rotation =
            Quaternion.LookRotation(angleText.transform.position - Camera.main.transform.position);

        textPlaced = true;
    }

    void DrawArc(Vector3 center, Vector3 startDirection, float angle)
    {
        arcRenderer.positionCount = arcResolution + 1;

        float step = angle / arcResolution;
        Vector3 axis = Vector3.Cross(startDirection, C.position - B.position);

        for (int i = 0; i <= arcResolution; i++)
        {
            Vector3 dir = Quaternion.AngleAxis(step * i, axis) * startDirection;
            arcRenderer.SetPosition(i, center + dir * 0.15f);
        }
    }

    public void Clear()
    {
        angleText.text = "";
        lineAB.positionCount = 0;
        lineCB.positionCount = 0;
        arcRenderer.positionCount = 0;
        A = B = C = null;
        textPlaced = false;
    }
}
