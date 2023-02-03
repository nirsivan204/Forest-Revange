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
        Vector2 treePosition;

        [SerializeField] private List<RootAgent> roots;
        [SerializeField] private GameObject tree;
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

        void Start()
        {
            treePosition = new Vector2(tree.transform.position.x, tree.transform.position.z);
            lastPlacedRootPosition = treePosition;
            Debug.Log(lastPlacedRootPosition);
        }
        
        void Update()
        {
            if (MouseInputManager.Instance.isDrawingLine)
            {
                Vector2 pos = new Vector2(MouseInputManager.Instance.hitPoint.x, MouseInputManager.Instance.hitPoint.z);
                UpdateRoot(pos);
                Debug.Log(pos);
            }
            
        }

        public void UpdateRoot(Vector2 position)
        {
            float distance = Vector2.Distance(position, treePosition);
            if (distance > maxExpansionDistance) return;

            distance = Vector2.Distance(position, lastPlacedRootPosition);
            if (distance > createRootMinimumDistance)
            {
                float rotation = Vector2.Angle(lastPlacedRootPosition, position);
                CreateRoot(position, rotation);
            }
        }

        private void CreateRoot(Vector2 position, float rotateBy)
        {
            Vector3 realPos = new Vector3(position.x, 3.45f, position.y);
             GameObject root = (GameObject)Instantiate(UnityEngine.Resources.Load("prefabs/Root"), realPos, Quaternion.identity);
             lastPlacedRootPosition = root.transform.position;
        }
    }
}


