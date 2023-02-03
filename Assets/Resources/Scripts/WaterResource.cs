using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterResource : ResourceEntity
{
    protected override void Start()
    {
        base.Start();
        type = ResourceType.Water;
    }
}
