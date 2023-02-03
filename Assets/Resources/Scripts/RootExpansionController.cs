using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Resources.Scripts
{
    public class RootExpansionController : MonoBehaviour
    {

        struct RootStruct{
            public MeshFilter mesh;
            public GameObject gameobject;

            public RootStruct(GameObject gameobject,MeshFilter mesh)
            {
                this.mesh = mesh;
                this.gameobject = gameobject;
            }
        }
        public static RootExpansionController Instance;

        private const float createRootMinimumDistance = 0.3f;
        private const int maxExpansionDistance = 5;
        Vector2 lastPlacedRootPosition;
        Vector2 treePosition;

        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameObject completeRoot;

        GameObject currentRoot = null;

        bool isUnderWorld = false;
        bool isBuildingRoot = false;

        List<RootStruct> rootsPositioned = new List<RootStruct>();

        private void OnEnable()
        {
            GameManager.changeWorldsEvent += OnChangeWorld;
            MouseInputManager.StartTargetUpdatedEvent += OnSrcTargetUpdated;
            MouseInputManager.EndTargetUpdatedEvent += OnEndTargetUpdated;
            MouseInputManager.ReleaseMouseButtonEvent += OnReleaseClick;

        }

        private void OnChangeWorld(World world)
        {
            isUnderWorld = world == World.Under;
        }

        private void OnDisable()
        {
            GameManager.changeWorldsEvent -= OnChangeWorld;
            MouseInputManager.StartTargetUpdatedEvent -= OnSrcTargetUpdated;
            MouseInputManager.EndTargetUpdatedEvent -= OnEndTargetUpdated;
            MouseInputManager.ReleaseMouseButtonEvent -= OnReleaseClick;


        }

        private void OnEndTargetUpdated(GameObject obj)
        {
            WaterResource resource = obj.GetComponent<WaterResource>();
            if (isBuildingRoot && resource && !resource.isCollected)
            {
                MergeRootMeshes();
                currentRoot.GetComponentInParent<TreeEntity>().AddWater(resource.amount);
                resource.isCollected = true;
                // ResourceManager.Collect(obj.GetComponent<ResourceEntity>());
            }
            else
            {
                StartCoroutine(DeletePlacedRoots());
            }
            isBuildingRoot = false;


        }

        private void MergeRootMeshes()
        {
            CombineInstance[] combine = new CombineInstance[rootsPositioned.Count];

            int i = 0;
            while (i < rootsPositioned.Count)
            {
                combine[i].mesh = rootsPositioned[i].mesh.sharedMesh;
                combine[i].transform = rootsPositioned[i].mesh.transform.localToWorldMatrix;
                rootsPositioned[i].mesh.gameObject.SetActive(false);

                i++;
            }
            Instantiate(completeRoot, transform);

            completeRoot.GetComponent<MeshFilter>().mesh = new Mesh();
            completeRoot.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
            completeRoot.SetActive(true);
            StartCoroutine(DeletePlacedRoots());
            

        }

        private void OnReleaseClick()
        {
            if (isBuildingRoot)
            {
                StartCoroutine(DeletePlacedRoots());
                isBuildingRoot = false;
            }

        }

        private void OnSrcTargetUpdated(GameObject target)
        {
            //if (!isBuildingRoot && MouseInputManager.Instance.isDrawingLine && MouseInputManager.Instance.target && MouseInputManager.Instance.target.tag == "UnderTree")
            if(target.tag == "UnderTree" && isUnderWorld)
            {
                currentRoot = target;
                treePosition = Vector3ToVector2(currentRoot.transform.position);// new Vector2(tree.transform.position.x, tree.transform.position.z);
                lastPlacedRootPosition = treePosition;
                isBuildingRoot = true;
            }
            else
            {
                isBuildingRoot = false;
                currentRoot = null;
            }
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

            Debug.Log(lastPlacedRootPosition);
        }
        
        void Update()
        {
            if (isUnderWorld)
            {

                if (isBuildingRoot && MouseInputManager.Instance.isDrawingLine)
                {
                    Vector2 pos = Vector3ToVector2(MouseInputManager.Instance.hitPoint);// new Vector2(MouseInputManager.Instance.hitPoint.x, MouseInputManager.Instance.hitPoint.z);
                    UpdateRoot(pos);
                    Debug.Log(pos);
                }
/*                else
                {
                    DeletePlacedRoots();
                    isBuildingRoot = false;
                }*/

            }



        }

        public void UpdateRoot(Vector2 position)
        {
            float distance = Vector2.Distance(position, treePosition);
            if (distance > maxExpansionDistance)
            {
                StartCoroutine(DeletePlacedRoots());

                isBuildingRoot = false;
                currentRoot = null;
                return;

            }

            distance = Vector2.Distance(position, lastPlacedRootPosition);
            if (distance > createRootMinimumDistance)
            {
                float rotation = Vector2.Angle(lastPlacedRootPosition, position);
                CreateRoot(position, rotation);
            }
        }
        WaitForSeconds waitCache = new WaitForSeconds(0.05f);

        IEnumerator DeletePlacedRoots()
        {
            for (int i = rootsPositioned.Count-1; i >= 0; i--)
            {
                try
                {
                    Destroy(rootsPositioned[i].gameobject);

                }
                catch (Exception)
                {
                    continue;
                }
                yield return waitCache;
            }
            rootsPositioned.Clear();
        }

        private void CreateRoot(Vector2 position, float rotateBy)
        {
            Vector3 realPos = Vector3ToVector2(position, 0.5f);
            GameObject root = (GameObject)Instantiate(UnityEngine.Resources.Load("prefabs/Root"), realPos, Quaternion.identity, this.transform);
            lastPlacedRootPosition = Vector3ToVector2(realPos);//root.transform.position;
            rootsPositioned.Add(new RootStruct(root,root.GetComponentInChildren<MeshFilter>()));

            if (rootsPositioned.Count > 1)
            {
                rootsPositioned[rootsPositioned.Count - 2].gameobject.transform.LookAt(realPos);
            }
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


