using UnityEngine;

namespace IsometricGrid.GridMaker
{
    public class GenerateGrid : MonoBehaviour
    {
        private CustomGrid.Grid _grid;
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
            _grid = new CustomGrid.Grid(Row, Col, CellSize, transform.position);
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

        //private void RightMouseClick()
        //{
        //    _grid.SetValue(MousePosition.position, 56);
        //}
        //private void LeftMouseClick()
        //{
        //    _grid.GetValue(MousePosition.position);
        //}
        public CustomGrid.Grid GetGrid() { return _grid; }
    }
}
