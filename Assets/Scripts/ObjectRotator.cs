using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public Transform pivot;
    public float rotationSpeed = 200f;
    private bool rotating;
    public bool allowRotation = true;

    void Update()
    {
        if (allowRotation)
        {
            if (Input.GetMouseButtonDown(1))
                rotating = true;

            if (Input.GetMouseButtonUp(1))
                rotating = false;

            if (rotating)
                Rotate();
        }


    }

    private void Rotate()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        pivot.Rotate(Vector3.up, -mx * rotationSpeed * Time.deltaTime, Space.World);
        pivot.Rotate(Vector3.right, my * rotationSpeed * Time.deltaTime, Space.World);
    }
}
