using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    int _waterAmount;
    int _treesAmount;
    

    public int WaterAmount { get => _waterAmount; set => _waterAmount = value; }
    public int TreesAmount { get => _treesAmount; set => _treesAmount = value; }

    public void AddWater(int amount)
    {
        _waterAmount += amount;
    }
    public void AddTrees(int amount)
    {
        _treesAmount += amount;
    }


}
