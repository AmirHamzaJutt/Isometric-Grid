using UnityEngine;

namespace IsometricGrid.CustomGrid
{
    public class Grid : MonoBehaviour
    {
        private int _col;
        private int _row;
        private float _cellSize;
        private int _totalTiles;
        private int[,] _gridArray;
        private Vector3 _originPosition;
        public Grid(int col, int row, float cellSize, Vector3 originPosition)
        {
            _col = col;
            _row = row;
            _cellSize = cellSize;
            _gridArray = new int[col, row];
            _originPosition = originPosition;
     
            DrawDebugLines();
        }
        private void DrawDebugLines()
        {
            for (int x = 0; x < _col; x++)
            {
                for (int y = 0; y < _row; y++)
                {
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.red,Mathf.Infinity);
                    Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.black, Mathf.Infinity);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, _row), GetWorldPosition(_col, _row), Color.red, Mathf.Infinity);
            Debug.DrawLine(GetWorldPosition(_col, 0), GetWorldPosition(_col, _row), Color.black, Mathf.Infinity);
        }
        public Vector3 GetWorldPosition(int x, int z)
        {
            return new Vector3(x, 0,z) * _cellSize+_originPosition;
        }
        public void GetGridPosition(Vector3 worldPosition, out int x, out int z)
        {
            x = Mathf.FloorToInt((worldPosition-_originPosition).x / _cellSize);
            z = Mathf.FloorToInt((worldPosition-_originPosition).z / _cellSize);
        }
        private void SetValue(int x, int y, int value)
        {
            if (x >= 0 && y >= 0 && x < _col && y < _row)
            {
                _gridArray[x, y] = value;
                Debug.LogError( "Set Value of Cell  (" + x + "," + y + ")=" + _gridArray[x,y]);
            }
        }
        public Vector2 FindSelectedCell(Vector3 worldPosition)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
          //  Debug.LogError("Selected Value of Cell  (" + x + "," + y + ")=" + _gridArray[x, y]);
            return new Vector2(x, y);
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
            for (int x = 0; x < _col; x++)
            {
                for (int y = 0; y < _row; y++)
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


        //Find AdjacentCells--------------------------------
        public Vector2 FindLeftCell(Vector3 worldPosition)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            if (x > 0)
            {
                int tempX = x - 1;
              //  Debug.LogError("Left Value of Cell  (" + tempX + "," + y + ")=" + _gridArray[tempX, y]);
                return new Vector2(tempX, y);
            }
            else
            {
               // Debug.LogError("Left Cell Not Exist");
                return new Vector2(-1, -1);
            }
        }
        public Vector2 FindRightCell(Vector3 worldPosition)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            if (x < _col - 1)
            {
                int tempX = x + 1;
               // Debug.LogError("Right Value of Cell  (" + tempX + "," + y + ")=" + _gridArray[tempX, y]);
                return new Vector2(tempX, y);
            }
            else
            {
               // Debug.LogError("Right Cell Not Exist");
                return new Vector2(-1, -1);
            }
        }
        public Vector2 FindTopCell(Vector3 worldPosition)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            if (y < _row - 1)
            {
                int tempY = y + 1;
               // Debug.LogError("Top Value of Cell  (" + x + "," + tempY + ")=" + _gridArray[x, tempY]);
                return new Vector2(x, tempY);
            }
            else
            {
              //  Debug.LogError("Top Cell Not Exist");
                return new Vector2(-1, -1);
            }
        }
        public Vector2 FindBottomCell(Vector3 worldPosition)
        {
            int x, y;
            GetGridPosition(worldPosition, out x, out y);
            if (y > 0)
            {
                int tempY = y - 1;
             //   Debug.LogError("Bottom Value of Cell (" + x + "," + tempY + ") = " + _gridArray[x, tempY]);
                return new Vector2(x, tempY);
            }
            else
            {
              //  Debug.LogError("Bottom Cell Not Exist");
                return new Vector2(-1, -1);
            }
        }

    }
}
