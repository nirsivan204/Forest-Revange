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
        LevelChanged.Invoke(this, _level);

    }
}
