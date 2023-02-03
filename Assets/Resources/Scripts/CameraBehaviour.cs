using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] GameObject targetGameObject;
    [SerializeField] float timeOffset;
    [SerializeField] Vector3 offset;
    [SerializeField] float cameraMoveSpeed = 4f;

    private Vector3 targetPosition;
    private bool upperWorldIsShown = true;
    private float minDistance = 0.1f;
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchGroundLayer();
        }
    }
    private void LateUpdate()
    {
        Vector3 updatedTargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Vector3.Distance(targetPosition, updatedTargetPosition) > minDistance)
        {
            Vector3 moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - targetPosition).normalized;
            MoveCamera(-moveDirection);
        }
    }
    private void MoveCamera(Vector3 moveDirection)
    {
        if (Input.GetMouseButton(1))
        {
            transform.position += moveDirection * cameraMoveSpeed * Time.deltaTime;
        }
    }
    private void SwitchGroundLayer()
    {
        upperWorldIsShown = !upperWorldIsShown;
    }
    private void FocusOnTargetGameObject()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = targetGameObject.transform.position;

        endPos.x += offset.x;
        endPos.y = offset.y;
        endPos.z += offset.z;

        Vector3 nextStep = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);
        transform.position = nextStep;
    }
}
