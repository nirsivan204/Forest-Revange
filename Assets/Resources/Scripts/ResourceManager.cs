using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    static int _waterAmount;
    static int _treesAmount;
    
    public static int WaterAmount { get => _waterAmount; set => _waterAmount = value; }
    public static int TreesAmount { get => _treesAmount; set => _treesAmount = value; }

    public static void AddWater(int amount)
    {
        _waterAmount += amount;
    }
    public static void AddTrees(int amount)
    {
        _treesAmount += amount;
    }


}
