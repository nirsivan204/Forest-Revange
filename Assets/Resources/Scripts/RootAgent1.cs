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

    public void SetGoal(Vector3 goal)
    {
        navMeshAgent.SetDestination(goal);
    }
}