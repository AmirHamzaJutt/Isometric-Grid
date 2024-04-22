using System;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace IsometricGrid.CustomGrid
{
    [Serializable]
    public class GridArray
    {
        public int X, Y;
        public int Occupied;
        public int Type;
        public GridArray(int x, int y, int type, int occupied)
        {
            X = x;
            Y = y;
            this.Type = type;
            Occupied = occupied;
        }
    }
    public class Grid : MonoBehaviour
    {
        private int _col;
        private int _row;
        private float _cellSize;
        private GridArray[,] _gridArray;
        private Vector3 _originPosition;
        public Grid(int col, int row, float cellSize, Vector3 originPosition)
        {
            _col = col;
            _row = row;
            _cellSize = cellSize;
            _gridArray = new GridArray[col, row];
            _originPosition = originPosition;
            createGrid();
            DrawDebugLines();
        }
        private void createGrid()
        {
            for (int x = 0; x < _col; x++)
            {
                for (int y = 0; y < _row; y++)
                {
                    _gridArray[x, y] = new GridArray(x, y, 0, 0);
                }
            }
        }
        private void DrawDebugLines()
        {
            for (int x = 0; x < _col; x++)
            {
                for (int y = 0; y < _row; y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.red, Mathf.Infinity);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.black, Mathf.Infinity);

                }
            }
            Debug.DrawLine(GetWorldPosition(0, _row), GetWorldPosition(_col, _row), Color.red, Mathf.Infinity);
            Debug.DrawLine(GetWorldPosition(_col, 0), GetWorldPosition(_col, _row), Color.black, Mathf.Infinity);
        }
        public Vector3 GetWorldPosition(int x, int z)
        {
            return new Vector3(x, 0, z) * _cellSize + _originPosition;
        }
        public void GetGridPosition(Vector3 worldPosition, out int x, out int z)
        {
            x = Mathf.FloorToInt((worldPosition - _originPosition).x / _cellSize);
            z = Mathf.FloorToInt((worldPosition - _originPosition).z / _cellSize);
        }
        private void SetValue(int x, int y, int value)
        {
            if (x >= 0 && y >= 0 && x < _col && y < _row)
            {
                _gridArray[x, y].Occupied = value;
                Debug.LogError("Set Value of Cell  (" + x + "," + y + ")=" + _gridArray[x, y].Occupied);
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
            if (x >= 0 && y >= 0 && x < _col && y < _row)
            {
                Debug.LogError("Get Value of Cell  (" + x + "," + y + ")=" + _gridArray[x, y].Occupied);
                return _gridArray[x, y].Occupied;
            }
            else
            {
                return 0;
            }
        }
        public int GetValue(Vector3 worldPosition)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            return GetValue(x, y);
        }
        public void DrawTiles(GridTile.Tile tile, Transform parent)
        {
            for (int x = 0; x < _col; x++)
            {
                for (int y = 0; y < _row; y++)
                {
                    GridTile.Tile tempTile = Instantiate(tile, GetWorldPosition(x, y), Quaternion.identity);
                    tempTile.TileType = DataReader.Json_Reader.Instance.GridDataa.TerrainGrid[x][y].TileType;
                    _gridArray[x, y].Type = DataReader.Json_Reader.Instance.GridDataa.TerrainGrid[x][y].TileType;
                    tempTile.transform.parent = parent;
                    tempTile.TileSize = _cellSize;
                }
            }
        }

        public Vector3 FindSelectedCell(Vector3 worldPosition, int type)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            Vector3 pos = GetWorldPosition(x, y);
            Vector3 centre = pos + new Vector3(_cellSize / 2f, 0f, _cellSize / 2f);
            return centre;
        }

        //Find AdjacentCells--------------------------------
        public Vector3 FindLeftCell(Vector3 worldPosition, int type)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            if (x > 0)
            {
                int tempX = x - 1;
                Vector3 pos = GetWorldPosition(tempX, y);
                Vector3 centre = pos + new Vector3(_cellSize / 2f, 0f, _cellSize / 2f);
                return centre;
            }
            else
            {
                return new Vector3(-1, -1, -1);
            }
        }
        public Vector3 FindRightCell(Vector3 worldPosition, int type)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            if (x < _col - 1)
            {
                int tempX = x + 1;
                Vector3 pos = GetWorldPosition(tempX, y);
                Vector3 centre = pos + new Vector3(_cellSize / 2f, 0f, _cellSize / 2f);
                return centre;
            }
            else
            {
                return new Vector3(-1, -1, -1);
            }
        }
        public Vector3 FindTopCell(Vector3 worldPosition, int type)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            if (y < _row - 1)
            {
                int tempY = y + 1;
                Vector3 pos = GetWorldPosition(x, tempY);
                Vector3 centre = pos + new Vector3(_cellSize / 2f, 0f, _cellSize / 2f);
                return centre;
            }
            else
            {
                return new Vector3(-1, -1, -1);
            }
        }
        public Vector3 FindBottomCell(Vector3 worldPosition, int type)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            if (y > 0)
            {
                int tempY = y - 1;
                Vector3 pos = GetWorldPosition(x, tempY);
                Vector3 centre = pos + new Vector3(_cellSize / 2f, 0f, _cellSize / 2f);
                return centre;
            }
            else
            {
                return new Vector3(-1, -1, -1);
            }
        }
        public bool GetOccupiedCell(Vector3 position)
        {
            int x, y;
            GetGridPosition(position, out x, out y);
            if (_gridArray[x, y].Occupied == 0)
            {
                return false;
            }
            else { return true; }

        }
        public void SetOccupiedCell(Vector3 position, int value)
        {
            int x = (int)position.x, y = (int)position.z;
            GetGridPosition(position, out x, out y);
            _gridArray[x, y].Occupied = value;
        }
        public int GetType(Vector3 position)
        {
            int x, y;
            GetGridPosition(position, out x, out y);
            return _gridArray[x, y].Type;
        }
    }
}
