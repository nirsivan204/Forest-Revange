using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeEntity : MonoBehaviour
{
    bool isUpgraded = false;
    [SerializeField] GameObject root;
    [SerializeField] GameObject seedling;
    [SerializeField] GameObject tree;
    [SerializeField] GameObject bounderies;
    [SerializeField] private GameObject boundryCylinder;
    internal bool connected;
    public PoolType connectedResource;
    public ResourceTypes type;
    [SerializeField] Material gas;
    [SerializeField] Material elec;
    [SerializeField] Material biuv;
    [SerializeField] Material waterTree;



    public void Start()
    {
        bounderies.transform.localScale = new Vector3(RootExpansionController.Instance.maxExpansionDistance*2, bounderies.transform.localScale.y, RootExpansionController.Instance.maxExpansionDistance*2);
    }
    private void OnEnable()
    {
        GameManager.changeWorldsEvent += OnChangeWorld;

    }

    private void OnChangeWorld(World world)
    {
        if(seedling != null)
        {
            seedling.SetActive(world == World.Upper);
        }
        boundryCylinder.SetActive(world == World.Upper);
    }

    private void OnDisable()
    {
        GameManager.changeWorldsEvent -= OnChangeWorld;


    }


    public void SetType(ResourceTypes type)
    {
        this.type = type;
        UpgradeTree();

    }

    private void UpgradeTree()
    {
        //GameObject.Find("GameManager").GetComponent<GameManager>().ChangeDimension();
        if (!isUpgraded)
        {
            StartCoroutine(CreateDustParticleEffect());
            Destroy(seedling);
            isUpgraded = true;
        }
        else
        {
            Destroy(tree);
        }
        tree = Instantiate((GameObject)Resources.Load("prefabs/Tree"), new Vector3(transform.position.x, 0, transform.position.z), transform.rotation, GameManager.Instance.UpperWorld.transform);
        tree.GetComponent<AppleThrow>().type = type;
        switch (type)
        {
            case ResourceTypes.Water:
                tree.GetComponentInChildren<SkinnedMeshRenderer>().material = waterTree;
                break;
            case ResourceTypes.Gas:
                //tree.GetComponentInChildren<SkinnedMeshRenderer>().material = (Material)Resources.Load("Tree revenger/Tree revenger Gas MAT.mat");
                tree.GetComponentInChildren<SkinnedMeshRenderer>().material = gas;
                break;
            case ResourceTypes.Sewage:
                tree.GetComponentInChildren<SkinnedMeshRenderer>().material = biuv;//(Material)Resources.Load("Tree revenger/Tree revenger Sewage MAT.mat");

                break;
            case ResourceTypes.Electricity:
                tree.GetComponentInChildren<SkinnedMeshRenderer>().material = elec;//(Material) Resources.Load("Tree revenger/Tree revenger Electricity MAT.mat");
                break;
            default:
                break;
        }

        Debug.Log("UPGRADE");
    }

    public void ToggleRange(bool state)
    {
        bounderies.SetActive(state);
    }

    public IEnumerator CreateDustParticleEffect()
    {
        Debug.Log("creating dust particle effect");
        GameObject dustEffect = Instantiate((GameObject)Resources.Load("Particle Effects/DustSmoke_A"), new Vector3(transform.position.x, 0, transform.position.z), transform.rotation, GameManager.Instance.UpperWorld.transform);
        Debug.Log("waiting 1 second");
        yield return new WaitForSeconds(1);
        Debug.Log("destroying dust particle effect");
        Destroy(dustEffect);
    }
}
