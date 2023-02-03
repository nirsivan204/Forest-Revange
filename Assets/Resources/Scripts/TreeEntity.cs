using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeEntity : MonoBehaviour
{
    float _connectedWaterAmount = 0;
    int _level = 0;
    [SerializeField] GameObject root;
    [SerializeField] GameObject seedling;
    [SerializeField] GameObject tree;
    [SerializeField] private float colliderRadiusMultiplayer = 3;

    public event EventHandler<int> LevelChanged;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void AddWater(float amount)
    {
        _connectedWaterAmount += amount;
        if(_level==0 && amount > Params.WaterToGrow)
        {
            UpgradeTree();
        }
    }

    private void UpgradeTree()
    {
        _level++;
        Destroy(seedling);
        tree = Instantiate((GameObject)Resources.Load("prefabs/Tree"),transform);
        Debug.Log("UPGRADE");
        LevelChanged?.Invoke(this, _level);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, colliderRadiusMultiplayer * _level);
        foreach (Collider collider in hitColliders)
        {
            TreeEntity collidedTree = collider.GetComponent<TreeEntity>();
            if (collidedTree)
            {
                if (collidedTree._level == _level && collidedTree != this)
                {
                    Destroy(collidedTree);
                    UpgradeTree();
                    return;
                }
                
            }
        }
    }
}
