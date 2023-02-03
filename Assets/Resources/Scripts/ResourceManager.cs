
using UnityEngine;

public static class ResourceManager
{
    static float _waterAmount;
    //static int _treesAmount;
    
    public static float WaterAmount { get => _waterAmount; set => _waterAmount = value; }
   //public static int TreesAmount { get => _treesAmount; set => _treesAmount = value; }

    public static void Collect(ResourceEntity resource)
    {
        switch (resource.type)
        {
            case ResourceType.Water:
                AddWater(resource.amount);
                resource.isCollected = true;
                break;
            default:
                break;
        }
    }


    public static float GetWaterAmount()
    {
        return _waterAmount;
    }

    public static void AddWater(float amount)
    {
        _waterAmount += amount;
    }

    public static void RemoveWater(float amount)
    {
        _waterAmount -= amount;
    }


}
