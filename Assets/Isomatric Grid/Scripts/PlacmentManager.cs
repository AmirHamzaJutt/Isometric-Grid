using IsometricGrid.DataReader;
using UnityEngine;
using IsometricGrid.GridMaker;
using System.Collections.Generic;

namespace IsometricGrid.PlacmentController
{
    public class PlacmentManager : MonoBehaviour
    {
        [SerializeField] private GameObject HorizontalPrefeb;
        [SerializeField] private GameObject VerticalPrefeb;

        private int _row;
        private int _col;

        private Vector3 _selectedCell;
        private Vector3 _leftCell;
        private Vector3 _rightCell;
        private Vector3 _bottomCell;
        private Vector3 _topCell;

        private List<GameObject> _allSpawnObjects = new List<GameObject>();

        private GenerateGrid _generateGrid;
        private CustomGrid.Grid _grid;

        private void Start()
        {
            _row = Json_Reader.instance.GridRows;
            _col = Json_Reader.instance.GridCols;
        }
        public void SetGrid(GenerateGrid grid)
        {
            _generateGrid = grid;
            _grid = _generateGrid.GetGrid();
        }
        public void PlaceObject(Vector3 position, int type)
        {
            _grid = _generateGrid.GetGrid();
            FindNeighbourWithGrid(position, type);
            int tileTypeLeft = _grid.GetType(_leftCell);
            int tileTypeRight = _grid.GetType(_rightCell);
            int tileTypeTop = _grid.GetType(_topCell);
            int tileTypeBottom = _grid.GetType(_bottomCell);

            bool occupiedCheckleft = _grid.GetOccupiedCell(_leftCell);
            bool occupiedCheckright = _grid.GetOccupiedCell(_rightCell);
            bool occupiedCheckTop = _grid.GetOccupiedCell(_topCell);
            bool occupiedCheckBottom = _grid.GetOccupiedCell(_bottomCell);


            _grid.SetOccupiedCell(_selectedCell, 1);
            if (_leftCell.x != -1 && tileTypeLeft == type && !occupiedCheckleft)
            {
                _grid.SetOccupiedCell(_leftCell, 1);
                GameObject tempObject = Instantiate(VerticalPrefeb, _selectedCell, Quaternion.identity);
                _allSpawnObjects.Add(tempObject);
            }
            else if (_rightCell.x != -1 && tileTypeRight == type && !occupiedCheckright)
            {
                _grid.SetOccupiedCell(_rightCell, 1);
                GameObject tempObject = Instantiate(VerticalPrefeb, _rightCell, Quaternion.identity);
                _allSpawnObjects.Add(tempObject);
            }
            else if (_topCell.x != -1 && tileTypeTop == type && !occupiedCheckTop)
            {
                _grid.SetOccupiedCell(_topCell, 1);
                GameObject tempObject = Instantiate(HorizontalPrefeb, _selectedCell, Quaternion.identity);
                _allSpawnObjects.Add(tempObject);
            }
            else if (_bottomCell.x != -1 && tileTypeBottom == type && !occupiedCheckBottom)
            {
                _grid.SetOccupiedCell(_bottomCell, 1);
                GameObject tempObject = Instantiate(HorizontalPrefeb, _bottomCell, Quaternion.identity);
                _allSpawnObjects.Add(tempObject);
            }
            else
            {
                Debug.LogError("No space Available for placing object");
            }
        }

        public void FindNeighbourWithGrid(Vector3 position, int type)
        {
            _selectedCell = _grid.FindSelectedCell(position, type);
            _leftCell = _grid.FindLeftCell(position, type);
            _rightCell = _grid.FindRightCell(position, type);
            _topCell = _grid.FindTopCell(position, type);
            _bottomCell = _grid.FindBottomCell(position, type);
        }
        public void ResetSpawnObject()
        {
            foreach (GameObject obj in _allSpawnObjects)
            {
                Destroy(obj);
            }
            for (int x = 0; x < _col; x++)
            {
                for (int y = 0; y < _row; y++)
                {
                    Vector3 pos = _grid.GetWorldPosition(x, y);
                    _grid.SetOccupiedCell(pos, 0);
                }
            }
            _allSpawnObjects.Clear();
        }
    }
}