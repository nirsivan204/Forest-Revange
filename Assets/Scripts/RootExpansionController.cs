using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootExpansionController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private List<RootAgent> roots;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if hit ground
            if (Physics.Raycast(ray, out hit))
            {
                int index = 0;
                float distance;
                float minDistance = 1000000;
                
                // find minimum distance root
                foreach (RootAgent root in roots)
                {
                    distance = Vector3.Distance(hit.point, transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        index = roots.IndexOf(root);
                    }
                }

                // set the root's goal to be the hit position
                roots[index].SetGoal(hit.point);
            }
        }
    }
}
