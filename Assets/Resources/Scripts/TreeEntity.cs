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

    public void Start()
    {
        bounderies.transform.localScale = new Vector3(RootExpansionController.Instance.maxExpansionDistance, bounderies.transform.localScale.y, RootExpansionController.Instance.maxExpansionDistance);
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
        tree = Instantiate((GameObject)Resources.Load("prefabs/Tree"), new Vector3(transform.position.x, 0, transform.position.z), transform.rotation, GameManager.Instance.UpperWorld.transform);
        tree.GetComponent<AppleThrow>().type = type;
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
