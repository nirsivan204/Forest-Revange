using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    public class RootExpansionController : MonoBehaviour
    {
        public static RootExpansionController Instance;
        
        private const int maxExpansionDistance = 5;
        
        private List<RootAgent> roots;
        [SerializeField] private Camera mainCamera;

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
        
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    RootAgent closestRoot = null;
                    float distance;
                    float minDistance = 1000000;

                    // find closest root to mouse click position
                    foreach (var root in roots)
                    {
                        distance = Vector3.Distance(root.transform.position, hit.point);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            closestRoot = root;
                        }
                    }
                    
                    if(minDistance<maxExpansionDistance)
                        closestRoot?.SetDestination(hit.point);
                }
            }
        }
    }
}


