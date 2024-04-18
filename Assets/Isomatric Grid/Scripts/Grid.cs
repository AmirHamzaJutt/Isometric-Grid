using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace IsometricGrid.Grid
{
    public class Grid : MonoBehaviour
    {
        private int _width;
        private int _height;
        private float _cellSize;
        private int _totalTiles;
        private int[,] _gridArray;
        private Vector3 _originPosition;
        public Grid(int width, int height, float cellSize, Vector3 originPosition)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _gridArray = new int[width, height];
            _originPosition = originPosition;
            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArray.GetLength(1); y++)
                {
                    Debug.LogError(_gridArray[x, y]);
                    Debug.DrawLine(GetWorlPosition(x, y), GetWorlPosition(x + 1, y), Color.red, 100f);
                    Debug.DrawLine(GetWorlPosition(x, y), GetWorlPosition(x, y + 1), Color.black, 100f);
                }
            }
            Debug.DrawLine(GetWorlPosition(0, height), GetWorlPosition(width, height), Color.red, 100f);
            Debug.DrawLine(GetWorlPosition(width, 0), GetWorlPosition(width, height), Color.black, 100f);
        }
        private Vector3 GetWorlPosition(int x, int z)
        {
            return new Vector3(x, 0,z) * _cellSize+_originPosition;
        }
        private void GetGridPosition(Vector3 worldPosition, out int x, out int z)
        {
            x = Mathf.FloorToInt((worldPosition-_originPosition).x / _cellSize);
            z = Mathf.FloorToInt((worldPosition-_originPosition).z / _cellSize);
        }
        private void SetValue(int x, int y, int value)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                _gridArray[x, y] = value;
                Debug.LogError(_gridArray[x, y] + "  (" + x + "," + y + ")");
            }
        }
        public void SetValue(Vector3 worldPosition, int value)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            SetValue(x, y, value);
        }
        private int GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                Debug.LogError(_gridArray[x, y] + "  (" + x + "," + y + ")");
                return _gridArray[x, y];
            }
            else
            {
                return 0;
            }
        }
        public int GetValue(Vector3 worldPosition)
        {
            int x, y;
            GetGridPosition(worldPosition,out x, out y);
            return GetValue(x, y);
        }
        public void DrawTiles(Tile.Tile tile)
        {
            
            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArray.GetLength(1); y++)
                {
                    Debug.LogError(_gridArray[x, y]);
                    Tile.Tile tempTile= Instantiate(tile, GetWorlPosition(x, y),Quaternion.identity);
                    tempTile.TileType = DataReader.Json_Reader.instance.GridDataa.TerrainGrid[x][y].TileType;
                    _totalTiles += 1;
                    tempTile._ID = _totalTiles;
                }
            }
            
        }
    }
}
