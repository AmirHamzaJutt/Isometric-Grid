
using IsometricGrid.GridTile;
using IsometricGrid.DataReader;
using UnityEngine;
using System;

public class PlacmentManager : MonoBehaviour
{
    [SerializeField] private GameObject PlacmentPrefeb;
    private int _row;
    private int _col;
    private void Start()
    {
        _row = Json_Reader.instance.GridRows;
        _col = Json_Reader.instance.GridCol;
        //Debug.LogError(_row + "   " + _col);
    }
    private enum _type { Dirt=0,Grass=1,Stone=2,Wood=3}
    [SerializeField] private _type TileType = _type.Wood;
    public void PlaceObject(Transform objectPosition,Tile tile)
    {
        if(!tile.TileOccupied && tile.TileType == (int)TileType)
        {
            Instantiate(PlacmentPrefeb, objectPosition.position, objectPosition.rotation);
            tile.TileOccupied = true;
            Debug.LogError("Object Placed successfully");
        }
        else
        {
            Debug.LogError("Can't able to placed Object");
        }
        FindNeighbors(tile);
    }
    public void FindNeighbors(Tile tile)
    {
        int index = tile.Id;
        Debug.LogError("tile index =" + index);
        if (index % _col != 0)
        {
            int leftNeihbour = index + 1;
            Debug.LogError("Riht Neighbors =" + leftNeihbour);
        }
        else
        {
            Debug.LogError("Riht Neighbors not available");
        }
        if(index % _col != 1) 
        {
            int leftNeihbour = index - 1;
            Debug.LogError("Left Neighbors =" + leftNeihbour);
        }
        else
        {
            Debug.LogError("Left Neighbors not available");
        }
        if (index < (_row*_col)-_row)
        {
            int bottomNeihbour = index + _col;
            Debug.LogError("Bottom Neighbors =" + bottomNeihbour);
        }
        else
        {
            Debug.LogError("Bottom Neighbors not available");
        }
        if(index > _col)
        {
            int topNeibour = index - _col;
            Debug.LogError("Top Neighbors =" + topNeibour);
        }
        else
        {
            Debug.LogError("Top Neighbors not available");
        }
    }
}
