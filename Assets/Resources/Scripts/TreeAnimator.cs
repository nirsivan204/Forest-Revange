using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimator : MonoBehaviour
{
    private MouseInputManager mouseInputManager;
    private AppleThrow appleThrowScript;
    private bool pullingTree;
    
    public Vector3 startPoint;

    // Start is called before the first frame update
    void Start()
    {
        mouseInputManager = GameObject.Find("Mouse Input Manager").gameObject.GetComponent<MouseInputManager>();
        appleThrowScript = transform.gameObject.GetComponent<AppleThrow>();
    }

    // Update is called once per frame
    void Update()
    {
        bool pullingTree = appleThrowScript.pullingTree;
        Vector3 startPoint = appleThrowScript.startPoint;
        Vector3 mousePoint = mouseInputManager.hitPoint;

        if (pullingTree)
        {
            float dragDistance = Vector3.Distance(startPoint, mousePoint);
            Vector3 dragDirection = Vector3.Normalize( mousePoint - startPoint);
            //Debug.Log(dragDirection);
        }
    }
}
