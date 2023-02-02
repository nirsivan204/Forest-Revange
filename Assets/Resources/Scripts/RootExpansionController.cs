using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    public class RootExpansionController : MonoBehaviour
    {
        public static RootExpansionController Instance;

        private const int createRootMinimumDistance = 1;
        private const int maxExpansionDistance = 5;
        Vector2 lastPlacedRootPosition;
        
        [SerializeField] private List<RootAgent> roots;
        [SerializeField] private RootAgent tree;
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
                //roots = FindObjectOfType(TreeManager).getTrees();
            }
        }
        
        void Update()
        {
            
        }

        public void UpdateRoot(Vector2 position)
        {
            float distance = Vector2.Distance(position, tree.transform.position);
            if (distance > maxExpansionDistance) return;

            distance = Vector2.Distance(position, lastPlacedRootPosition);
            if (distance < createRootMinimumDistance)
            {
                float rotation = Vector2.Angle(lastPlacedRootPosition, position);
                CreateRoot(position, rotation);
            }
        }

        private void CreateRoot(Vector2 position, float rotateBy)
        {
             GameObject root = (GameObject)Instantiate(UnityEngine.Resources.Load("/prefabs/Root"), position, Quaternion.identity);
             lastPlacedRootPosition = root.transform.position;
        }
    }
}


