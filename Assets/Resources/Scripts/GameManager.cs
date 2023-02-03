using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum World
{
    Under,
    Upper,
}

public class GameManager : MonoBehaviour
{
    public static Action<World> changeWorldsEvent;
    World _currentWorld = World.Upper;

    public void OnEnable()
    {
        changeWorldsEvent += OnWorldChange;
    }

    public void OnDisable()
    {
        changeWorldsEvent -= OnWorldChange;

    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _currentWorld = _currentWorld == World.Under ? World.Upper : World.Under;
            changeWorldsEvent.Invoke(_currentWorld);
        }

    }

    public void OnWorldChange(World world)
    {

    }

}
