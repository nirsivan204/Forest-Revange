using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreesManager : MonoBehaviour
{
    List<TreeScript> _treesList = new List<TreeScript>();

    public static Action<int> TreeSizeUpgradeEvent;


    internal List<TreeScript> TreesList { get => _treesList; set => _treesList = value; }

    public void AddTree(TreeScript tree)
    {
        _treesList.Add(tree);
    }

    

}
