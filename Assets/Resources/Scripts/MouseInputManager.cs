using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputManager : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Vector3 mouseClickPosition;
    private bool isDrawingLine;

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
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Clicked on object: " + hit.collider.gameObject.name);
                mouseClickPosition = hit.point;
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
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                lineRenderer.SetPosition(0, mouseClickPosition);
                lineRenderer.SetPosition(1, hit.point);
            }
        }
    }
}
