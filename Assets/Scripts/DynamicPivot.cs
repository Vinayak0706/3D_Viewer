using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicPivot : MonoBehaviour
{
    public Transform pivot;
    public Transform model;
    public GameObject markerPrefab;
    private GameObject marker;

    private bool allowPivotChange;
    private bool isPivotChangeButtonActive = false;

    public Image pivotChangeButtonImage;
    public Sprite pivotChangeOnSprite;
    public Sprite pivotChangeOffSprite;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && allowPivotChange)
            SelectPivot();
    }

    void SelectPivot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("No collider hit — check colliders and camera tag!");
            return;
        }

        Debug.Log("Hit: " + hit.collider.name);

        Vector3 oldModelPos = model.position;
        Quaternion oldRot = model.rotation;
        Vector3 oldScale = model.localScale;

        pivot.position = hit.point;

        model.position = oldModelPos;
        model.rotation = oldRot;
        model.localScale = oldScale;

        if (!marker) marker = Instantiate(markerPrefab);
        marker.transform.position = hit.point;
        marker.transform.SetParent(model);

    }

    public void TogglePivotChangeButton()
    {
        if (isPivotChangeButtonActive == false)
        {
            allowPivotChange = true;
            isPivotChangeButtonActive = true;
            pivotChangeButtonImage.sprite = pivotChangeOnSprite;
        }
        else
        {
            allowPivotChange = false;
            isPivotChangeButtonActive = false;
            pivotChangeButtonImage.sprite = pivotChangeOffSprite;
        }
    }
}
