using UnityEngine;
using UnityEngine.AI;

public class RootAgent : MonoBehaviour
{
    [SerializeField] private Transform goal;
    [SerializeField] private Camera mainCamera;

    private NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                navMeshAgent.SetDestination(hit.point);
            }

        }
    }
}