using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    public class RootExpansionController : MonoBehaviour
    {
        public static RootExpansionController Instance;

        private const float createRootMinimumDistance = 0.3f;
        private const int maxExpansionDistance = 5;
        Vector2 lastPlacedRootPosition;
        Vector2 treePosition;

        [SerializeField] private List<RootAgent> roots;
        [SerializeField] private GameObject tree;
        [SerializeField] private Camera mainCamera;

        bool isUnderWorld = false;

        private void OnEnable()
        {
            GameManager.changeWorldsEvent += OnChangeWorld;
        }

        private void OnChangeWorld(World world)
        {
            isUnderWorld = world == World.Under;
        }

        private void OnDisable()
        {
            GameManager.changeWorldsEvent -= OnChangeWorld;

        }


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
            treePosition = Vector3ToVector2(tree.transform.position);// new Vector2(tree.transform.position.x, tree.transform.position.z);
            lastPlacedRootPosition = treePosition;
            Debug.Log(lastPlacedRootPosition);
        }
        
        void Update()
        {
            if (isUnderWorld && MouseInputManager.Instance.isDrawingLine)
            {
                Vector2 pos = Vector3ToVector2(MouseInputManager.Instance.hitPoint);// new Vector2(MouseInputManager.Instance.hitPoint.x, MouseInputManager.Instance.hitPoint.z);
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
            Vector3 realPos = Vector3ToVector2(position, 0.5f);
            GameObject root = (GameObject)Instantiate(UnityEngine.Resources.Load("prefabs/Root"), realPos, Quaternion.identity, this.transform);
            lastPlacedRootPosition = Vector3ToVector2(realPos);//root.transform.position;
        }

        private Vector2 Vector3ToVector2(Vector3 vector)
        {
            return new Vector2(vector.x, vector.z);
        }

        private Vector3 Vector3ToVector2(Vector2 vector, float height)
        {
            return new Vector3(vector.x,height, vector.y);

        }
    }
}


