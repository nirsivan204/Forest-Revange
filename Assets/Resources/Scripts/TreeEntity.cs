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
    [SerializeField] private float colliderRadiusMultiplayer = 5;

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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, colliderRadiusMultiplayer * _level);
        Debug.Log("colliders in sphere: " + hitColliders.Length);
        foreach (Collider collider in hitColliders)
        {
            TreeEntity collidedTree = collider.GetComponentInParent<TreeEntity>();
            if (collidedTree && collidedTree != this)
            {
                Debug.Log("Found tree in nearby colliders");

                if (collidedTree._level == _level && collidedTree != this)
                {
                    RootExpansionController.Instance.MergeRoots(collidedTree.root.transform.position, this.root.transform.position);//  collidedTree.root.transform
                    Destroy(collidedTree.tree);
                    Destroy(collidedTree.gameObject);
                    UpgradeTree();
                    return;
                }
                
            }
        }
    }
}
