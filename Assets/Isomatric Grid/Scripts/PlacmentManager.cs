using IsometricGrid.GridTile;
using IsometricGrid.DataReader;
using UnityEngine;
using Unity.VisualScripting;

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
    private void Start()
    {
        _row = Json_Reader.instance.GridRows;
        _col = Json_Reader.instance.GridCol;
    }

    public void PlaceObject(GameObject grid, int index,int availableType)
    {
        FindNeighbors(index, grid, availableType);

        _selectedGameobject.IsOccupied = true;
        if(_leftNeihbour!=null || _rightNeihbour!=null)
        {
            //horizontal
            Debug.LogError("horizontal object placed");
            //Instantiate(HorizontalPrefeb, _selectedGameobject.transform.position, _selectedGameobject.transform.rotation);
        }
        else if(_topNeihbour!=null || _bottomNeihbour!=null)
        {
            //vertical
            Debug.LogError("vertical object placed");

            // Instantiate(VerticalPrefeb, _selectedGameobject.transform.position, _selectedGameobject.transform.rotation);
        }
    }
    public void FindNeighbors(int index,GameObject grid, int availableType)
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

        _selectedGameobject= grid.transform.GetChild(index-1).GetComponent<Tile>();
        if (index % _col != 0)
        {
            _rightNeihbourIndex = index + 1;
            Tile tempNeihbour = grid.transform.GetChild(_rightNeihbourIndex - 1).GetComponent<Tile>();
            if (tempNeihbour.TileType == availableType)
            {
                Debug.LogError(tempNeihbour.TileType);
                _rightNeihbour = tempNeihbour;
            }
        }
        if (index % _col != 1)
        {
            _leftNeihbourIndex = index - 1;
            Tile tempNeihbour = grid.transform.GetChild(_leftNeihbourIndex - 1).GetComponent<Tile>();
            if (tempNeihbour.TileType == availableType)
            {
                Debug.LogError(tempNeihbour.TileType);
                _leftNeihbour = tempNeihbour;
            }
        }
        if (index <= (_row * _col) - _col)
        {
            _bottomNeihbourIndex = index + _col;
            Tile tempNeihbour = grid.transform.GetChild(_bottomNeihbourIndex - 1).GetComponent<Tile>();
            if (tempNeihbour.TileType == availableType)
            {
                Debug.LogError(tempNeihbour.TileType);
                _bottomNeihbour = tempNeihbour;
            }
        }
        if (index > _col)
        {
            _topNeihbourIndex = index - _col;
            Tile tempNeihbour = grid.transform.GetChild(_topNeihbourIndex - 1).GetComponent<Tile>();
            if (tempNeihbour.TileType == availableType)
            {
                Debug.LogError(tempNeihbour.TileType);
                _topNeihbour = tempNeihbour;
            }
        }
        Debug.LogError("Selected index=["+index+"]    left["+_leftNeihbourIndex+"]    right["+_rightNeihbourIndex+"]    bottom["+_bottomNeihbourIndex+"]    top["+_topNeihbourIndex+"]" );
    }
}
