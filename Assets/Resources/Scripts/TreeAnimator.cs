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

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.gameObject.GetComponentInChildren<Animator>();
        animator.speed = 0;
        mouseInputManager = GameObject.Find("Mouse Input Manager").gameObject.GetComponent<MouseInputManager>();
        appleThrowScript = transform.gameObject.GetComponent<AppleThrow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (animationTime <= 1)
        {
            animationTime = animationTime + Time.deltaTime;
        }
        animator.Play(0, 0, animationTime);
        bool pullingTree = appleThrowScript.pullingTree;
        Vector3 startPoint = appleThrowScript.startPoint;
        Vector3 dragDirection = appleThrowScript.dragDirection;
        Vector3 mousePoint = mouseInputManager.hitPoint;
        
        if (pullingTree)
        {
            float dragDistance = Vector3.Distance(startPoint, mousePoint);
            
            //Debug.Log(dragDirection);
        }
    }
}
