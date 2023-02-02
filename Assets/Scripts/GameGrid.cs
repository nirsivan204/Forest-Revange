using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TilesTypes
{
    None,
    Water,
    Root,
    Tree
}

public abstract class AbstractGameGrid : MonoBehaviour
{
    [SerializeField] int Width;
    [SerializeField] int Height;

    private TilesTypes[,] GameBoard;

    public virtual void init()
    {

    }

    public TilesTypes GetTile(int x,int y)
    {
        return GameBoard[x, y];
    }

    public void SetTile(int x, int y, TilesTypes type)
    {

    }

}
