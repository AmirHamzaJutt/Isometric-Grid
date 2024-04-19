using IsometricGrid.GridTile;
using IsometricGrid.DataReader;
using UnityEngine;
using IsometricGrid.GenerateGrid;
using System.Collections.Generic;

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

    public Tile _rightNeihbour;
    public Tile _leftNeihbour;
    public Tile _bottomNeihbour;
    public Tile _topNeihbour;
    public Tile _selectedGameobject;

    public List<GameObject> _allSpawnObjects;
    private GenerateGrid _generateGrid;
    private void Start()
    {
        _row = Json_Reader.instance.GridRows;
        _col = Json_Reader.instance.GridCol;
    }

    public void PlaceObject(GenerateGrid grid, int index, int availableType)
    {
        _generateGrid = grid;
        FindNeighbors(index, availableType);

        _selectedGameobject.IsOccupied = true;
        if (_leftNeihbour != null || _rightNeihbour != null)
        {
            Debug.LogError("horizontal object placed");
            Collider colider;
            if (_leftNeihbour != null)
            {
                _leftNeihbour.IsOccupied = true;
                colider = _leftNeihbour.GetComponent<Collider>();
            }
            else
            {
                _rightNeihbour.IsOccupied = true;
                colider = _selectedGameobject.GetComponent<Collider>();
            }
          
            Vector3 center = colider.bounds.center;
            Debug.LogError("Get Centrre of ="+ colider);
            GameObject tempObject = Instantiate(HorizontalPrefeb, center, _selectedGameobject.transform.rotation);

            _allSpawnObjects.Add(tempObject);
        }
        else if (_topNeihbour != null || _bottomNeihbour != null)
        {
            Debug.LogError("vertical object placed");
            Collider colider;
            if (_topNeihbour != null)
            {
                _topNeihbour.IsOccupied = true;
                colider=_selectedGameobject.GetComponent<Collider>();
            }
            else
            {
                _bottomNeihbour.IsOccupied = true;
                colider=_bottomNeihbour.GetComponent<Collider> ();
            }
            
            Vector3 center = colider.bounds.center;
            GameObject tempObject = Instantiate(VerticalPrefeb, center, _selectedGameobject.transform.rotation);
            _allSpawnObjects.Add(tempObject);
        }
        else
        {
            Debug.LogError("No space Available for placing object");
        }
    }
    public void FindNeighbors(int index, int availableType)
    {
        _leftNeihbourIndex = -1;
        _rightNeihbourIndex = -1;
        _bottomNeihbourIndex = -1;
        _topNeihbourIndex= -1;

        _topNeihbour = null;
        _bottomNeihbour= null;
        _leftNeihbour = null;
        _rightNeihbour=null;
        _selectedGameobject = null;

        _selectedGameobject= _generateGrid.transform.GetChild(index-1).GetComponent<Tile>();
        if (index % _col != 0)
        {
            _rightNeihbourIndex = index + 1;
            Tile tempNeihbour = _generateGrid.transform.GetChild(_rightNeihbourIndex - 1).GetComponent<Tile>();
            if (tempNeihbour.TileType == availableType && !tempNeihbour.IsOccupied)
            {
                Debug.LogError(tempNeihbour.TileType);
                _rightNeihbour = tempNeihbour;
            }
        }
        if (index % _col != 1)
        {
            _leftNeihbourIndex = index - 1;
            Tile tempNeihbour = _generateGrid.transform.GetChild(_leftNeihbourIndex - 1).GetComponent<Tile>();
            if (tempNeihbour.TileType == availableType && !tempNeihbour.IsOccupied)
            {
                Debug.LogError(tempNeihbour.TileType);
                _leftNeihbour = tempNeihbour;
            }
        }
        if (index <= (_row * _col) - _col)
        {
            _bottomNeihbourIndex = index + _col;
            Tile tempNeihbour = _generateGrid.transform.GetChild(_bottomNeihbourIndex - 1).GetComponent<Tile>();
            if (tempNeihbour.TileType == availableType && !tempNeihbour.IsOccupied)
            {
                Debug.LogError(tempNeihbour.TileType);
                _bottomNeihbour = tempNeihbour;
            }
        }
        if (index > _col)
        {
            _topNeihbourIndex = index - _col;
            Tile tempNeihbour = _generateGrid.transform.GetChild(_topNeihbourIndex - 1).GetComponent<Tile>();
            if (tempNeihbour.TileType == availableType && !tempNeihbour.IsOccupied)
            {
                Debug.LogError(tempNeihbour.TileType);
                _topNeihbour = tempNeihbour;
            }
        }
        Debug.LogError("Selected index=["+index+"]    left["+_leftNeihbourIndex+"]    right["+_rightNeihbourIndex+"]    bottom["+_bottomNeihbourIndex+"]    top["+_topNeihbourIndex+"]" );
    }
    public void ResetSpawnObject()
    {
        foreach (GameObject obj in _allSpawnObjects)
        {
            Destroy(obj);
        }
        for (int i = 0; i < _row*_col; i++)
        {
            _generateGrid.transform.GetChild(i).GetComponent<Tile>().IsOccupied = false;
        }
        _allSpawnObjects.Clear();
    }
}
