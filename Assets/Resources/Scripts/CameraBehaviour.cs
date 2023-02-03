using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] GameObject targetGameObject;
    [SerializeField] float timeOffset;
    [SerializeField] Vector3 offset;
    [SerializeField] float cameraMoveSpeed = 4f;

    [SerializeField] GameObject _upperWorld;
    [SerializeField] GameObject _underWorld;
    float _upperWorldHeight;
    float _underWorldHeight;

    private Vector3 targetPosition;
    private float minDistance = 0.1f;
    private bool isChangingWorlds = false;

    public void Start()
    {
        _upperWorldHeight = offset.y + _upperWorld.transform.position.y;
        _underWorldHeight = offset.y + _underWorld.transform.position.y;

    }

    public void OnEnable()
    {
        GameManager.changeWorldsEvent += OnWorldChange;
    }

    public void OnDisable()
    {
        GameManager.changeWorldsEvent -= OnWorldChange;

    }

    public void OnWorldChange(World world)
    {

        isChangingWorlds = true;
        targetPosition = new Vector3(transform.position.x, world == World.Upper ? _upperWorldHeight : _underWorldHeight, transform.position.z);

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 updatedTargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector3.Distance(targetPosition, updatedTargetPosition) > minDistance)
            {
                Vector3 moveDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - targetPosition).normalized;
                MoveCamera(-moveDirection);
            }
        }
        else
        {
            if (isChangingWorlds && Mathf.Abs(transform.position.y - targetPosition.y)>0.01f)
            {
                MoveCamera((transform.position.y - targetPosition.y)*Vector3.down);
            }
            else
            {
                isChangingWorlds = false;
            }

        }

    }
    private void MoveCamera(Vector3 moveDirection)
    {
        //if (Input.GetMouseButton(1))
        {
            transform.position += moveDirection * cameraMoveSpeed * Time.deltaTime;
        }
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

    private void MoveToTarget(Vector3 target)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = target;
    }
}
