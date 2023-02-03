using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class AbstractGameGrid : MonoBehaviour
{
    [SerializeField] int Width;
    [SerializeField] int Height;

    public void OnEnable()
    {
        GameManager.changeWorldsEvent += OnWorldChange;
    }

    public void OnDisable()
    {
        GameManager.changeWorldsEvent -= OnWorldChange;

    }

    protected abstract void OnWorldChange(World world);

    public virtual void init()
    {

    }

}
