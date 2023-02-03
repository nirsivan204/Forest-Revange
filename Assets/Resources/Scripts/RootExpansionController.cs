using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class RootExpansionController : MonoBehaviour
    {
        public static RootExpansionController Instance;

        struct RootStruct{
            public MeshFilter mesh;
            public GameObject gameobject;

            public RootStruct(GameObject gameobject,MeshFilter mesh)
            {
                this.mesh = mesh;
                this.gameobject = gameobject;
            }
        }

        private const float createRootMinimumDistance = 0.3f;
        private const int maxExpansionDistance = 5;
        Vector2 lastPlacedRootPosition;
        Vector2 treePosition;

        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameObject completeRoot;
    TreeEntity _currentRootEntity;

    GameObject currentRoot = null;

        bool isUnderWorld = false;
        bool isBuildingRoot = false;

        List<RootStruct> LastRootsPositioned = new List<RootStruct>();
        List<RootStruct> currerntRootsPositioned = new List<RootStruct>();


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
            //MergeRootMeshes();

            if (_currentRootEntity.connected)
            {
                StartCoroutine(DeletePlacedRoots(false));

            }
                resource.isCollected = true;
                _currentRootEntity.connected = true;
                LastRootsPositioned = currerntRootsPositioned;
                currerntRootsPositioned = new List<RootStruct>();
            if (_currentRootEntity.connectedResource != null)
            {
                _currentRootEntity.connectedResource.isCollected = false;
            }
                _currentRootEntity.connectedResource = resource;
            _currentRootEntity.AddWater(5);

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
            CombineInstance[] combine = new CombineInstance[currerntRootsPositioned.Count];

            int i = 0;
            while (i < currerntRootsPositioned.Count)
            {
                combine[i].mesh = currerntRootsPositioned[i].mesh.sharedMesh;
                combine[i].transform = currerntRootsPositioned[i].mesh.transform.localToWorldMatrix;
                currerntRootsPositioned[i].mesh.gameObject.SetActive(false);

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
            _currentRootEntity = target.GetComponentInParent<TreeEntity>();
                treePosition = Vector3ToVector2(currentRoot.transform.position);// new Vector2(tree.transform.position.x, tree.transform.position.z);
                lastPlacedRootPosition = treePosition;
                isBuildingRoot = true;
            
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
                CreateRoot(position);
            }
        }
        WaitForSeconds waitCache = new WaitForSeconds(0.05f);

        IEnumerator DeletePlacedRoots(bool isCurrent = true)
        {
        var rootsToDelete = isCurrent ? currerntRootsPositioned : LastRootsPositioned;

            for (int i = rootsToDelete.Count-1; i >= 0; i--)
            {
                try
                {
                    Destroy(rootsToDelete[i].gameobject);

                }
                catch (Exception)
                {
                    continue;
                }
                yield return waitCache;
            }
            rootsToDelete.Clear();
        }

        private void CreateRoot(Vector2 position)
        {
            Vector3 realPos = Vector3ToVector2(position, -1f);
            GameObject root = (GameObject)Instantiate(UnityEngine.Resources.Load("prefabs/Root"), realPos, Quaternion.identity, this.transform);
            lastPlacedRootPosition = Vector3ToVector2(realPos);//root.transform.position;
            currerntRootsPositioned.Add(new RootStruct(root,root.GetComponentInChildren<MeshFilter>()));

            if (currerntRootsPositioned.Count > 1)
            {
                currerntRootsPositioned[currerntRootsPositioned.Count - 2].gameobject.transform.LookAt(realPos);
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

    public void MergeRoots(Vector3 posA,Vector3 posB)
    {
        float distance = Vector3.Distance(posA, posB);
        Vector2 diraction = Vector3ToVector2(posB - posA).normalized;
        for (int i = 0; i < distance/ createRootMinimumDistance;i++)
        {
            CreateRoot(Vector3ToVector2(posA) + diraction * createRootMinimumDistance*i);
        }
        currerntRootsPositioned.Clear();
        isBuildingRoot = false;
        currentRoot = null;

    }    
}


