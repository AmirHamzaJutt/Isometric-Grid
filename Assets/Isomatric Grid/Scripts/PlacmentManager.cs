using IsometricGrid.GridTile;
using IsometricGrid.DataReader;
using UnityEngine;
using IsometricGrid.GridMaker;
using System.Collections.Generic;
using IsometricGrid.CustomGrid;
using TMPro;
using System.Runtime.CompilerServices;

namespace IsometricGrid.PlacmentController
{
    public class PlacmentManager : MonoBehaviour
    {
        [SerializeField] private GameObject HorizontalPrefeb;
        [SerializeField] private GameObject VerticalPrefeb;

        private int _row;
        private int _col;

       // private int _rightNeihbourIndex;
      //  private int _leftNeihbourIndex;
      //  private int _bottomNeihbourIndex;
      //  private int _topNeihbourIndex;

      //  private Tile _selectedGameobject;
      //  private Tile _rightNeihbour;
      //  private Tile _leftNeihbour;
      //  private Tile _bottomNeihbour;
       // private Tile _topNeihbour;

        public Vector3 _selectedCell;
        public Vector3 _leftCell;
        public Vector3 _rightCell;
        public Vector3 _bottomCell;
        public Vector3 _topCell;
        
        public List<GameObject> _allSpawnObjects;

        private GenerateGrid _generateGrid;
        private CustomGrid.Grid _grid;

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
                Debug.LogError("horizontal Left Side object placed");
                _grid.SetOccupiedCell(_leftCell, 1);
                GameObject tempObject = Instantiate(VerticalPrefeb, _selectedCell, Quaternion.identity);
                _allSpawnObjects.Add(tempObject);
            }
            else if (_rightCell.x != -1 && tileTypeRight == type && !occupiedCheckright)
            {
                Debug.LogError("horizontal Right Side object placed" + tileTypeRight);
                _grid.SetOccupiedCell(_rightCell, 1);
                GameObject tempObject = Instantiate(VerticalPrefeb, _rightCell, Quaternion.identity);
                _allSpawnObjects.Add(tempObject);
            }
            else if (_topCell.x != -1 && tileTypeTop == type && !occupiedCheckTop)
            {
                Debug.LogError("vertical top object placed");
                _grid.SetOccupiedCell(_topCell, 1);
                GameObject tempObject = Instantiate(HorizontalPrefeb, _selectedCell, Quaternion.identity);
                _allSpawnObjects.Add(tempObject);
            }
            else if (_bottomCell.x != -1 && tileTypeBottom == type && !occupiedCheckBottom)
            {
                Debug.LogError("vertical Bottom object placed");
                _grid.SetOccupiedCell(_bottomCell, 1);
                GameObject tempObject = Instantiate(HorizontalPrefeb, _bottomCell, Quaternion.identity);
                _allSpawnObjects.Add(tempObject);
            }
            else
            {
                Debug.LogError("No space Available for placing object");
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
        public void FindNeighbourWithGrid(Vector3 position, int type)
        {
            
            _selectedCell = _grid.FindSelectedCell(position,type);
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
            for (int i = 0; i < _row * _col; i++)
            {
                //_generateGrid.transform.GetChild(i).GetComponent<Tile>().IsOccupied = false;
            }
            _allSpawnObjects.Clear();
        }
    }
}