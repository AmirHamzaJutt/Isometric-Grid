using UnityEngine;
using UnityEditor;


namespace IsometricGrid.GridMaker
{
    public class GenerateGrid : MonoBehaviour
    {
        private CustomGrid.Grid _grid;
        [SerializeField] private Transform Highliter;
        [SerializeField] private GridTile.Tile TilePrefab;
        [SerializeField] private int Row;
        [SerializeField] private int Col;
        [SerializeField] private float CellSize;
        private DataReader.Json_Reader _reader;
        void Start()
        {
            _reader = DataReader.Json_Reader.instance;
            Row = _reader.GridRows;
            Col = _reader.GridCol;
            _grid = new CustomGrid.Grid(Col, Row, CellSize, transform.position);
            DrawTilesOnGrid();
        }
        void DrawTilesOnGrid()
        {
            _grid.DrawTiles(TilePrefab,transform);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            for (int x = 0; x < Col; x++)
            {
                for (int y = 0; y < Row; y++)
                {
                    Vector3 cellPosition = _grid. GetWorldPosition(x, y);
                    Vector3 textPosition = cellPosition + new Vector3(0, 0f, CellSize / 2f);
                    DrawCellText(textPosition, $"{x},{y}" +"  Occupied="+_grid.GetOccupiedCell(textPosition));
                }
            }
        }
        private void DrawCellText(Vector3 position, string text)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;
            Handles.Label(position, text, style);
            Debug.LogError("draw text");
        }
        public CustomGrid.Grid GetGrid() 
        {
            return _grid; 
        }
    }
}
