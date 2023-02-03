using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeScript : MonoBehaviour
{
    private int waterAmount = 0;
    private int stage = 1;
    private UnityEvent growTreeEvent;
    private List<ResourceEntity> waterResources;

    private void GrowTree()
    {
        stage++;
        growTreeEvent.Invoke();
    }

    private void CollectWater(int amount)
    {
        waterAmount += amount;
    }

    private bool UseWater(int amount)
    {
        if (waterAmount >= amount)
        {
            waterAmount -= amount;
            return true;
        }

        return false;
    }

    public void AddWaterResource(ResourceEntity resource)
    {
        waterResources.Add(resource);
    }

    public void AddGrowTreeEventListener(UnityAction action)
    {
        growTreeEvent.AddListener(action);
    }
}