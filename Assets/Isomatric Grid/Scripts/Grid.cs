using UnityEngine;
using UnityEditor;

namespace IsometricGrid.CustomGrid
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
     
            DrawDebugLines();
        }
        private void DrawDebugLines()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.red,Mathf.Infinity);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.black, Mathf.Infinity);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, _height), GetWorldPosition(_width, _height), Color.red, Mathf.Infinity);
            Debug.DrawLine(GetWorldPosition(_width, 0), GetWorldPosition(_width, _height), Color.black, Mathf.Infinity);
        }
        private void OnDrawGizmos()
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    Vector3 cellPosition = GetWorldPosition(x, y);
                    Handles.Label(cellPosition, $"{x},{y}");
                }
            }
        }
        private Vector3 GetWorldPosition(int x, int z)
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
                Debug.LogError( "Set Value of Cell  (" + x + "," + y + ")=" + _gridArray[x,y]);
            }
        }
        public Vector2 FindLeftCell(Vector3 worldPosition)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
           // if(x)
            return new Vector2();
        }

        public void SetValue(Vector3 worldPosition, int value)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            Debug.LogError(x+","+ y);
            SetValue(x, y, value);
        }
        private int GetValue(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < _width && y < _height)
            {
                Debug.LogError("Get Value of Cell  (" + x + "," + y + ")=" + _gridArray[x, y]);
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
        public void DrawTiles(GridTile.Tile tile,Transform parent)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    _totalTiles += 1;
                    GridTile.Tile tempTile= Instantiate(tile, GetWorldPosition(x, y),Quaternion.identity);
                    tempTile.TileType = DataReader.Json_Reader.instance.GridDataa.TerrainGrid[x][y].TileType;
                    tempTile. transform.parent = parent;
                    tempTile.Id = _totalTiles;
                    tempTile.TileSize = _cellSize;
                    tempTile.name = _totalTiles.ToString();
                }
            }   
        }
  
    }
}
