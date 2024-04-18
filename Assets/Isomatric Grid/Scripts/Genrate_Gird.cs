using IsometricGrid.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsometricGrid.GenerateGrid
{
    public class GenerateGrid : MonoBehaviour
    {
        private Grid.Grid _grid;
        [SerializeField] private Transform MousePosition;
        [SerializeField] private Tile.Tile TilePrefab;
        [SerializeField] private int Row;
        [SerializeField] private int Col;
        [SerializeField] private float CellSize;
        private DataReader.Json_Reader _reader;
        private void OnEnable()
        {
            InputSystem.Input_System.RightMouseClick += RightMouseClick;
            InputSystem.Input_System.LeftMouseClick += LeftMouseClick;
        }
        private void Awake()
        {
            _reader = DataReader.Json_Reader.instance;
        }
        void Start()
        {
            GetData();
            _grid = new Grid.Grid(Row, Col, CellSize, transform.position);
            DrawTilesOnGrid();
        }
        void GetData()
        {
            Row = _reader.GridDataa.TerrainGrid.Length;
            Col = _reader.GridDataa.TerrainGrid[0].Length;
        }
        void DrawTilesOnGrid()
        {
            _grid.DrawTiles(TilePrefab);
        }
        private void RightMouseClick()
        {
            _grid.SetValue(MousePosition.position, 56);
        }
        private void LeftMouseClick()
        {
            _grid.GetValue(MousePosition.position);
        }
    }
}
