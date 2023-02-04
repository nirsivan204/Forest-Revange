using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimator : MonoBehaviour
{
    private float animationTime = 0;

    public Animator animator;

    private MouseInputManager mouseInputManager;
    private AppleThrow appleThrowScript;
    private bool pullingTree; 
    
    public Vector3 startPoint;


    // Defining the bone anchor points
    private Transform midBone;
    private Transform topBone;

    private float rotatedX = 0;
    private float rotatedY = 0;

    private float rotateZ = 0;
    private float rotateX = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.gameObject.GetComponentInChildren<Animator>();
        animator.speed = 0;
        mouseInputManager = GameObject.Find("Mouse Input Manager").gameObject.GetComponent<MouseInputManager>();
        appleThrowScript = transform.gameObject.GetComponent<AppleThrow>();

        // Defining the bone anchor points
        midBone = transform.GetChild(0).Find("Tree_ctrl").GetChild(0).GetChild(0).GetChild(0);
        topBone = midBone.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTime <= 1)
        {
            animationTime = animationTime + Time.deltaTime;
            animator.Play(0, 0, animationTime);

        }
        else
        {
            animator.enabled = false;
        }
        bool pullingTree = appleThrowScript.pullingTree;
        Vector3 startPoint = appleThrowScript.startPoint;
        Vector3 dragDirection = appleThrowScript.dragDirection;
        Vector3 mousePoint = mouseInputManager.hitPoint;
        
        if (pullingTree)
        {
            float dragDistance = Vector3.Distance(startPoint, mousePoint);
            dragDirection = Vector3.ClampMagnitude(mousePoint - startPoint, appleThrowScript.maxThrowMagnitute);

            rotateZ = (dragDirection.x / appleThrowScript.maxThrowMagnitute) * 45;
            rotateX = (dragDirection.z / appleThrowScript.maxThrowMagnitute) * 45;


            //midBone.rota
            //Debug.Log(dragDirection);
        }
        else
        {
            rotateZ = Mathf.LerpAngle(rotateZ, 0, 0.5f);
            rotateX = Mathf.LerpAngle(rotateX, 0, 0.5f);

        }
        midBone.eulerAngles = new Vector3(rotateX, 0, -rotateZ);
        topBone.eulerAngles = new Vector3(rotateX, 0, -rotateZ);
    }
}
