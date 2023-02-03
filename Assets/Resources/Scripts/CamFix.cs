using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFix : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] float cameraMoveSpeed = 4f;
    [SerializeField] float timeOffset;

    [SerializeField] GameObject _upperWorld;
    [SerializeField] GameObject _underWorld;
    float _upperWorldHeight;
    float _underWorldHeight;

    private Vector3 targetPosition;
    private Vector3 dragOrigin = Vector3.zero;

    void Start()
    {
        targetPosition = transform.position;
        _upperWorldHeight = offset.y + _upperWorld.transform.position.y;
        _underWorldHeight = offset.y + _underWorld.transform.position.y;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(1))
        {
            Drag();
        }
    }

    void LateUpdate()
    {
        MoveToTarget(targetPosition);
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
        targetPosition.y = world == World.Upper ? _upperWorldHeight : _underWorldHeight;
    }
    private void Drag()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
            targetPosition.x = dragOrigin.x - difference.x;
            targetPosition.z = dragOrigin.z - difference.z;    
    }
    private void MoveToTarget(Vector3 target)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = target;

        endPos.x += offset.x;
        endPos.z += offset.z;

        Vector3 nextStep = Vector3.Lerp(startPos, endPos, cameraMoveSpeed * timeOffset * Time.deltaTime);
        transform.position = targetPosition;
    }
}
