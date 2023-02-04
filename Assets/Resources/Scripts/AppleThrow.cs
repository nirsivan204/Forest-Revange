using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleThrow : MonoBehaviour
{
    public float maxClickDistanceToRegisterThrow = 1;
    public float minDragDistanceToRegisterThrow = 1;

    private MouseInputManager mouseInputManager;

    public bool pullingTree = false;

    private Vector3 startPoint;

    private GameObject apple;

    public float maxThrowMagnitute = 2f;

    public ResourceTypes type;


    // Start is called before the first frame update
    void Start()
    {
        mouseInputManager = GameObject.Find("Mouse Input Manager").gameObject.GetComponent<MouseInputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mouseInputManager.isDrawingLine && !pullingTree)
        {
            startPoint = mouseInputManager.hitPoint;
            if(Vector3.Distance(startPoint, transform.position) <= maxClickDistanceToRegisterThrow)
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

                GameObject newApple = Instantiate(apple, transform.position + new Vector3(0,5,0), transform.rotation, transform.parent);
                Vector3 dragDirection = Vector3.ClampMagnitude(endPoint - startPoint, maxThrowMagnitute);
                newApple.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), ForceMode.Impulse);
                newApple.GetComponent<Rigidbody>().AddForce(dragDirection * -1 + new Vector3(0,dragDirection.magnitude/2,0), ForceMode.Impulse);
            }
            pullingTree = false;
        }
    }
}
