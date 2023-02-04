using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleThrow : MonoBehaviour
{
    public float maxClickDistanceToRegisterThrow = 1;
    public float minDragDistanceToRegisterThrow = 1;

    private MouseInputManager mouseInputManager;

    public bool pullingTree = false;

    public Vector3 startPoint;

    private GameObject apple;

    public float maxThrowMagnitute = 2f;

    public ResourceTypes type;
    private bool canThrow = true;
    private bool isUpper = true;

    public Vector3 dragDirection = new Vector3(0, 0, 0);

    private void OnEnable()
    {
        GameManager.changeWorldsEvent += OnChangeWorld;
        MouseInputManager.StartTargetUpdatedEvent += setIsShooting;
    }

    private void OnChangeWorld(World world)
    {
        isUpper = world == World.Upper;
        if (!isUpper)
        {
            canThrow = false;
        }
    }

    private void OnDisable()
    {
        GameManager.changeWorldsEvent -= OnChangeWorld;
        MouseInputManager.StartTargetUpdatedEvent -= setIsShooting;


    }

    private void setIsShooting(GameObject go)
    {
        if (isUpper) 
        {
            canThrow = true;
        }
        else
        {
            canThrow = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        mouseInputManager = GameObject.Find("Mouse Input Manager").gameObject.GetComponent<MouseInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canThrow)
        {
            if (mouseInputManager.isDrawingLine && !pullingTree)
            {
                startPoint = mouseInputManager.hitPoint;
                Vector3 treePositionOnOffsetPlane = new Vector3(transform.position.x, 5, transform.position.z);
                if (Vector3.Distance(startPoint, treePositionOnOffsetPlane) <= maxClickDistanceToRegisterThrow)
                {
                    pullingTree = true;
                }

            }

            if (!mouseInputManager.isDrawingLine && pullingTree)
            {
                Vector3 endPoint = mouseInputManager.hitPoint;
                if (Vector3.Distance(startPoint, endPoint) > minDragDistanceToRegisterThrow)
                {
                    // TODO: THROW APPLE OF RIGHT TYPE
                    switch (type)
                    {
                        case ResourceTypes.Gas:
                            apple = Resources.Load<GameObject>("prefabs/Apples/Gas Apple");
                            break;
                        case ResourceTypes.Sewage:
                            apple = Resources.Load<GameObject>("prefabs/Apples/Sewage Apple");
                            break;
                        case ResourceTypes.Electricity:
                            apple = Resources.Load<GameObject>("prefabs/Apples/Electricity Apple");
                            break;
                        case ResourceTypes.Water:
                            apple = Resources.Load<GameObject>("prefabs/Apples/Water Apple");
                            break;
                    }

                    GameObject newApple = Instantiate(apple, transform.position + new Vector3(0, 5, 0), transform.rotation, transform.parent);
                    dragDirection = Vector3.ClampMagnitude(endPoint - startPoint, maxThrowMagnitute);
                    newApple.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), ForceMode.Impulse);
                    newApple.GetComponent<Rigidbody>().AddForce(dragDirection * -1 + new Vector3(0, dragDirection.magnitude / 2, 0), ForceMode.Impulse);
                }
                pullingTree = false;
            }
        }

    }
}
