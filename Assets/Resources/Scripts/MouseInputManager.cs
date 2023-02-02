using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputManager : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Vector3 mouseClickPosition;
    private bool isDrawingLine;

    public float targetHeight = 5;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0, targetHeight, 0));
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                mouseClickPosition = hitPoint;
                isDrawingLine = true;
                lineRenderer.enabled = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && isDrawingLine)
        {
            isDrawingLine = false;
            lineRenderer.enabled = false;
        }

        if (isDrawingLine)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, new Vector3(0, targetHeight, 0));
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                lineRenderer.SetPosition(0, mouseClickPosition);
                lineRenderer.SetPosition(1, hitPoint);
            }
        }
    }
}