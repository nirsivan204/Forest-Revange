using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerType
{
    NoPower,
    Gas,
    Sewerage,
    Electric,
}

public class TreeEntity : MonoBehaviour
{
    float _connectedWaterAmount = 0;
    int _level = 0;
    PowerType power  = PowerType.NoPower;
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
        if(_level==0 && _connectedWaterAmount >= Params.WaterToGrow)
        {
            UpgradeTree();
        }
    }

    private void UpgradeTree()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().ChangeDimension();
        _level++;
        Debug.Log("new level: " + _level);
        if (_level == 1)
        {
            Destroy(seedling);
            tree = Instantiate((GameObject)Resources.Load("prefabs/Tree"), new Vector3(transform.position.x, 0, transform.position.z), transform.rotation, GameObject.Find("Upper World").transform);    
        }
        Debug.Log("UPGRADE");
        LevelChanged?.Invoke(this, _level);
    }

    public PowerType getPower()
    {
        return power;
    }

}
