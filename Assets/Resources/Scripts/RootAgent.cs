using UnityEngine;
using UnityEngine.AI;

namespace Assets.Resources.Scripts
{
    public class RootAgent : MonoBehaviour
    {
        private NavMeshAgent rootNavMeshAgent;
    
        private void Start()
        {
            rootNavMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void SetDestination(Vector3 target)
        {
            rootNavMeshAgent.destination = target;
        }
    }
}