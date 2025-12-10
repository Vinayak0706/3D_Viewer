using UnityEngine;
using UnityEngine.UI;

public class PointPicker : MonoBehaviour
{
    public Camera cam;
    public LayerMask selectableLayer;
    public GameObject pointPrefab;
    public AngleVisualizer visualizer;
    public Image measureButtonImage;
    public Sprite measure;
    public Sprite clearMeasure;
    public ObjectRotator objectRotator;

    private Transform[] selectedPoints = new Transform[3];
    private int index = 0;
    private bool measurementActive = false;
    private bool toggleMeasurements = false;


    private readonly System.Collections.Generic.List<Transform> spawnedPoints = new();

    void Update()
    {
        if (!measurementActive) return; // <-- prevent unwanted clicks

        if (Input.GetMouseButtonDown(0) && index < 3)
        {
            if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100f, selectableLayer))
            {
                GameObject p = Instantiate(pointPrefab, hit.point, Quaternion.identity);
                spawnedPoints.Add(p.transform);

                selectedPoints[index] = p.transform;
                index++;

                if (index == 3)
                {
                    visualizer.SetPoints(selectedPoints[0], selectedPoints[1], selectedPoints[2]);
                    measurementActive = false; // stop further clicks
                }
            }
        }
    }

    // 🟢 Called by a UI button
    public void StartMeasurement()
    {
        ClearPoints();
        measurementActive = true;
    }

    // 🧹 Called by a UI button
    public void ClearPoints()
    {
        foreach (var p in spawnedPoints)
            if (p) Destroy(p.gameObject);

        spawnedPoints.Clear();
        
        index = 0;
        measurementActive = false;

        visualizer.Clear();
        measurementActive = false;
    }

    public void ToogleMesurement()
    {
        if (toggleMeasurements == false)
        {
            StartMeasurement();
            toggleMeasurements = true;
            measureButtonImage.sprite = clearMeasure;
            objectRotator.allowRotation = false;
        }
        else
        {
            ClearPoints();
            toggleMeasurements = false;
            measureButtonImage.sprite = measure;
            objectRotator.allowRotation = true;
        }

    }
}
