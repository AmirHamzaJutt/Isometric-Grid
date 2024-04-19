using UnityEngine;

namespace IsometricGrid.GenerateGrid
{
    public class GenerateGrid : MonoBehaviour
    {
        private Grid.Grid _grid;
        [SerializeField] private Transform MousePosition;
        [SerializeField] private GridTile.Tile TilePrefab;
        [SerializeField] private int Row;
        [SerializeField] private int Col;
        [SerializeField] private float CellSize;
        private DataReader.Json_Reader _reader;
        void Start()
        {
            _reader = DataReader.Json_Reader.instance;
            GetData();
            _grid = new Grid.Grid(Row, Col, CellSize, transform.position);
            DrawTilesOnGrid();
        }
        void GetData()
        {
            Row = _reader.GridRows;
            Col = _reader.GridCol;
        }
        void DrawTilesOnGrid()
        {
            _grid.DrawTiles(TilePrefab,transform);
        }
    }
}
