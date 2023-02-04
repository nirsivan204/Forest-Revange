using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputManager : MonoBehaviour
{
    public static MouseInputManager Instance;
    public string nameOfGameObjectToRespondTo = "";

    private LineRenderer lineRenderer;

    private Vector3 mouseClickPosition;
    public bool isDrawingLine;
    public Vector3 hitPoint;

    [SerializeField] float underWorldHeight = -1;
    [SerializeField] float upperWorldHeight = 0;
    Plane _upperWorldPlane;
    Plane _underWorldPlane;
    Plane _currentPlane;

    public GameObject target;

    public static Action<GameObject> StartTargetUpdatedEvent;
    public static Action<GameObject> EndTargetUpdatedEvent;
    public static Action ReleaseMouseButtonEvent;


    private void OnEnable()
    {
        GameManager.changeWorldsEvent += OnChangeWorld;
    }
    int layerMask;
    private void OnChangeWorld(World world)
    {
        _currentPlane = world == World.Under? _underWorldPlane : _upperWorldPlane;
        layerMask = world == World.Under ? ~(1<<6) : 0;
    }

    private void OnDisable()
    {
        GameManager.changeWorldsEvent -= OnChangeWorld;

    }

    void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        _upperWorldPlane = new Plane(Vector3.up, new Vector3(0, upperWorldHeight, 0));
        _underWorldPlane = new Plane(Vector3.up, new Vector3(0, underWorldHeight, 0));
        _currentPlane = _upperWorldPlane;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        target = transform.gameObject;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //We now check if we where we click on the screen and project that point at a certain height for rendering.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = _currentPlane; // Generating a plane at this desired rendering height.
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                target = ClickTarget(ray); // THIS IS THE IMPORTANT PART! Here we actually test what is it that we hit on the ground.
                Vector3 hitPoint = ray.GetPoint(distance);
                mouseClickPosition = hitPoint;
                isDrawingLine = true;
                lineRenderer.enabled = true;
            }
        }

        if (target && target.name != nameOfGameObjectToRespondTo)
        {
            lineRenderer.enabled = false;
        }

        if (Input.GetMouseButtonUp(0) && isDrawingLine)
        {
            isDrawingLine = false;
            lineRenderer.enabled = false;
            ReleaseMouseButtonEvent?.Invoke();
        }

        if (isDrawingLine)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane plane = _currentPlane;
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                hitPoint = ray.GetPoint(distance);
                lineRenderer.SetPosition(0, mouseClickPosition);
                lineRenderer.SetPosition(1, hitPoint);
                GameObject currentTarget = HoverTarget(ray); // THIS IS THE IMPORTANT PART! Here we actually test what is it that we hit on the ground.
                if (currentTarget && currentTarget.tag == "Resource")
                {
                    EndTargetUpdatedEvent?.Invoke(currentTarget);
                }

            }
        }
    }

    GameObject HoverTarget(Ray ray)
    {
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit,Mathf.Infinity, layerMask))
        {
            return hit.collider.gameObject;
        }
        return null;
    }

        GameObject ClickTarget(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,Mathf.Infinity,layerMask))
        {
            Debug.Log("Clicked on object: " + hit.collider.gameObject.name);
            mouseClickPosition = hit.point;
            isDrawingLine = true;
            lineRenderer.enabled = true;
            StartTargetUpdatedEvent?.Invoke(hit.collider.gameObject);
            return (hit.collider.gameObject);
        }
        return null;
    }
}