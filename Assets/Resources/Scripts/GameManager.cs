using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum World
{
    Under,
    Upper,
}

public enum ResourceTypes
{
    Water,
    Gas,
    Sewage,
    Electricity,
}

public class GameManager : MonoBehaviour
{
    public static Action<World> changeWorldsEvent;
    World _currentWorld = World.Upper;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeDimension();
        }

    }

    public void ChangeDimension()
    {
        _currentWorld = _currentWorld == World.Under ? World.Upper : World.Under;
        changeWorldsEvent.Invoke(_currentWorld);
    }

}
