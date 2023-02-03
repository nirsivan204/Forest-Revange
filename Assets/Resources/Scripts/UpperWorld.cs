using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperWorld : AbstractGameGrid
{
    //[SerializeField] GameObject _meshParent;
    float hideOpacity = 0.25f;
    protected override void OnWorldChange(World world)
    {
        if(world == World.Under)
        {
            SetInvisible(hideOpacity, false);
        }
        else
        {
            SetInvisible(1, true);
        }
    }

    private void SetInvisible(float opacity, bool v)
    {

    }
}
