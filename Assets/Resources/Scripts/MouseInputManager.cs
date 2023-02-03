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

    public float targetHeight = 5;

    [SerializeField] float underWorldHeight = 1;
    [SerializeField] float upperWorldHeight = 5;


    public GameObject target;
    public static Action<GameObject> targetUpdatedEvent;

    private void OnEnable()
    {
        GameManager.changeWorldsEvent += OnChangeWorld;
    }

    private void OnChangeWorld(World world)
    {
        targetHeight = world == World.Under? underWorldHeight:upperWorldHeight;
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
            Plane plane = new Plane(Vector3.up, new Vector3(0, targetHeight, 0)); // Generating a plane at this desired rendering height.
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

        if (target.name != nameOfGameObjectToRespondTo)
        {
            lineRenderer.enabled = false;
        }

        if (isDrawingLine)
        {

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
                hitPoint = ray.GetPoint(distance);
                lineRenderer.SetPosition(0, mouseClickPosition);
                lineRenderer.SetPosition(1, hitPoint);
                
            }
        }
    }

    GameObject ClickTarget(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Clicked on object: " + hit.collider.gameObject.name);
            mouseClickPosition = hit.point;
            isDrawingLine = true;
            lineRenderer.enabled = true;
            targetUpdatedEvent?.Invoke(hit.collider.gameObject);
            return (hit.collider.gameObject);
        }
        return null;
    }
}