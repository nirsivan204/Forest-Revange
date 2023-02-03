using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperWorld : AbstractGameGrid
{
    [SerializeField] GameObject _meshParent;
    protected override void OnWorldChange(World world)
    {
        if(world == World.Under)
        {
            SetInvisible(true);
        }
        else
        {
            SetInvisible(false);
        }
    }

    private void SetInvisible(bool v)
    {
        _meshParent.SetActive(!v);
    }
}
