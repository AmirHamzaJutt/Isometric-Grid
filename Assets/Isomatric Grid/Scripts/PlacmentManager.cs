using IsometricGrid.GridTile;
using IsometricGrid.DataReader;
using UnityEngine;
using IsometricGrid.GridMaker;
using System.Collections.Generic;
using IsometricGrid.CustomGrid;

namespace IsometricGrid.PlacmentController
{
    public class PlacmentManager : MonoBehaviour
    {
        [SerializeField] private GameObject HorizontalPrefeb;
        [SerializeField] private GameObject VerticalPrefeb;

        private int _row;
        private int _col;

        private int _rightNeihbourIndex;
        private int _leftNeihbourIndex;
        private int _bottomNeihbourIndex;
        private int _topNeihbourIndex;

        private Tile _selectedGameobject;
        private Tile _rightNeihbour;
        private Tile _leftNeihbour;
        private Tile _bottomNeihbour;
        private Tile _topNeihbour;

        public Vector2 _selectedCell;
        public Vector2 _leftCell;
        public Vector2 _rightCell;
        public Vector2 _bottomCell;
        public Vector2 _topCell;

        public List<GameObject> _allSpawnObjects;

        public GenerateGrid _generateGrid;
        public CustomGrid.Grid _grid;

        private void Start()
        {
            _row = Json_Reader.instance.GridRows;
            _col = Json_Reader.instance.GridCol;
        }
        public void SetGrid(GenerateGrid grid)
        {
            _generateGrid= grid;
            _grid = _generateGrid.GetGrid();
        }
        public void PlaceObject(Vector3 position)
        {
            _grid = _generateGrid.GetGrid();
            FindNeighbourWithGrid(position);
            if (_grid.GetValue(position)==0)
            {
                _grid.SetValue(_selectedCell, 1);
                if (_leftCell.x != -1 || _rightCell.x != -1)
                {
                    Debug.LogError("horizontal object placed");
                    Collider colider;
                    if (_leftCell.x != -1)
                    {
                        _grid.SetValue(_leftCell, 1);
                        //  colider = _leftNeihbour.GetComponent<Collider>();
                    }
                    else
                    {
                        _grid.SetValue(_rightCell, 1);
                        //colider = _selectedGameobject.GetComponent<Collider>();
                    }
                    // Vector3 center = colider.bounds.center;
                    // GameObject tempObject = Instantiate(HorizontalPrefeb, center, _selectedGameobject.transform.rotation);
                    // _allSpawnObjects.Add(tempObject);
                }
                if (_topCell.x != -1 || _bottomCell.x != -1)
                {
                    Debug.LogError("vertical object placed");
                    if (_bottomCell.x != -1)
                    {
                        _grid.SetValue(_bottomCell, 1);
                        //colider = _bottomNeihbour.GetComponent<Collider>();
                    }
                    else
                    {
                        _grid.SetValue(_topCell, 1);
                        // colider = _selectedGameobject.GetComponent<Collider>();
                    }
                    //Vector3 center = colider.bounds.center;
                    //GameObject tempObject = Instantiate(VerticalPrefeb, center, _selectedGameobject.transform.rotation);
                    // _allSpawnObjects.Add(tempObject);
                }
                else
                {
                    Debug.LogError("No space Available for placing object");
                }
            }
            else
            {
                Debug.LogError("Cell occupied");
            }
        }
        //public void PlaceObject(GenerateGrid grid, int index, int availableType)
        //{
       
        //  //  FindNeighbors(index, availableType);

        //    _selectedGameobject.IsOccupied = true;
        //    if (_leftNeihbour != null || _rightNeihbour != null)
        //    {
        //        Debug.LogError("horizontal object placed");
        //        Collider colider;
        //        if (_leftNeihbour != null)
        //        {
        //            _leftNeihbour.IsOccupied = true;
        //            colider = _leftNeihbour.GetComponent<Collider>();
        //        }
        //        else
        //        {
        //            _rightNeihbour.IsOccupied = true;
        //            colider = _selectedGameobject.GetComponent<Collider>();
        //        }

        //        Vector3 center = colider.bounds.center;
        //        GameObject tempObject = Instantiate(HorizontalPrefeb, center, _selectedGameobject.transform.rotation);

        //        _allSpawnObjects.Add(tempObject);
        //    }
        //    else if (_topNeihbour != null || _bottomNeihbour != null)
        //    {
        //        Debug.LogError("vertical object placed");
        //        Collider colider;
        //        if (_topNeihbour != null)
        //        {
        //            _topNeihbour.IsOccupied = true;
        //            colider = _selectedGameobject.GetComponent<Collider>();
        //        }
        //        else
        //        {
        //            _bottomNeihbour.IsOccupied = true;
        //            colider = _bottomNeihbour.GetComponent<Collider>();
        //        }

        //        Vector3 center = colider.bounds.center;
        //        GameObject tempObject = Instantiate(VerticalPrefeb, center, _selectedGameobject.transform.rotation);
        //        _allSpawnObjects.Add(tempObject);
        //    }
        //    else
        //    {
        //        Debug.LogError("No space Available for placing object");
        //    }
        //}
        //public void FindNeighbors(int index, int availableType)
        //{
        //    _leftNeihbourIndex = -1;
        //    _rightNeihbourIndex = -1;
        //    _bottomNeihbourIndex = -1;
        //    _topNeihbourIndex = -1;

        //    _topNeihbour = null;
        //    _bottomNeihbour = null;
        //    _leftNeihbour = null;
        //    _rightNeihbour = null;
        //    _selectedGameobject = null;

        //    _selectedGameobject = _generateGrid.transform.GetChild(index - 1).GetComponent<Tile>();
        //    if (index % _col != 0)
        //    {
        //        _rightNeihbourIndex = index + 1;
        //        Tile tempNeihbour = _generateGrid.transform.GetChild(_rightNeihbourIndex - 1).GetComponent<Tile>();
        //        if (tempNeihbour.TileType == availableType && !tempNeihbour.IsOccupied)
        //        {
        //            _rightNeihbour = tempNeihbour;
        //        }
        //    }
        //    if (index % _col != 1)
        //    {
        //        _leftNeihbourIndex = index - 1;
        //        Tile tempNeihbour = _generateGrid.transform.GetChild(_leftNeihbourIndex - 1).GetComponent<Tile>();
        //        if (tempNeihbour.TileType == availableType && !tempNeihbour.IsOccupied)
        //        {
        //            _leftNeihbour = tempNeihbour;
        //        }
        //    }
        //    if (index <= (_row * _col) - _col)
        //    {
        //        _bottomNeihbourIndex = index + _col;
        //        Tile tempNeihbour = _generateGrid.transform.GetChild(_bottomNeihbourIndex - 1).GetComponent<Tile>();
        //        if (tempNeihbour.TileType == availableType && !tempNeihbour.IsOccupied)
        //        {
        //            _bottomNeihbour = tempNeihbour;
        //        }
        //    }
        //    if (index > _col)
        //    {
        //        _topNeihbourIndex = index - _col;
        //        Tile tempNeihbour = _generateGrid.transform.GetChild(_topNeihbourIndex - 1).GetComponent<Tile>();
        //        if (tempNeihbour.TileType == availableType && !tempNeihbour.IsOccupied)
        //        {
        //            _topNeihbour = tempNeihbour;
        //        }
        //    }
        //    //Debug.LogError("Selected index=[" + index + "]    left[" + _leftNeihbourIndex + "]    right[" + _rightNeihbourIndex + "]    bottom[" + _bottomNeihbourIndex + "]    top[" + _topNeihbourIndex + "]");
        //}
        public void FindNeighbourWithGrid(Vector3 position)
        {
            
            _selectedCell = _grid.FindSelectedCell(position);
            _leftCell = _grid.FindLeftCell(position);
            _rightCell = _grid.FindRightCell(position);
            _topCell = _grid.FindTopCell(position);
            _bottomCell = _grid.FindBottomCell(position);
        }
        public void ResetSpawnObject()
        {
            foreach (GameObject obj in _allSpawnObjects)
            {
                Destroy(obj);
            }
            for (int i = 0; i < _row * _col; i++)
            {
                //_generateGrid.transform.GetChild(i).GetComponent<Tile>().IsOccupied = false;
            }
            _allSpawnObjects.Clear();
        }
    }
}