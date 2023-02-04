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
    internal bool connected;
    public PoolType connectedResource;
    public ResourceTypes type;

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
            Destroy(seedling);
            isUpgraded = true;
        }
        tree = Instantiate((GameObject)Resources.Load("prefabs/Tree"), new Vector3(transform.position.x, 0, transform.position.z), transform.rotation, GameManager.Instance.UpperWorld.transform);
        tree.GetComponent<AppleThrow>().type = type;
        Debug.Log("UPGRADE");
    }
}
