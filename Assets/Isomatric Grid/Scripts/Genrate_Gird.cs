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

        private void OnEnable()
        {
            InputSystem.Input_System.RightMouseClick += RightMouseClick;
            InputSystem.Input_System.LeftMouseClick += LeftMouseClick;
        }
        //  [SerializeField] private int _totalCell;
        void Start()
        {
            _grid = new Grid.Grid(Row, Col, CellSize, transform.position);
            // _tileSize = _tilePrefab.TileSize;
            // DrawGrid();
            DrawTilesOnGrid();
        }
        //void DrawGrid()
        //{
        //    for (int i = 0; i < _row; i++)
        //    {
        //        Vector3 offsetZ = new Vector3(0, 0, _tileSize) * i;

        //        for (int j = 0; j < _column; j++)
        //        {
        //            Tile.Tile tempTile = Instantiate(_tilePrefab, transform.position, transform.rotation);
        //            tempTile.transform.parent = transform;
        //            Vector3 offsetX = new Vector3(_tileSize, 0, 0) * j;
        //            tempTile.transform.localPosition = offsetX + offsetZ;
        //            _totalCell += 1;
        //            tempTile._ID = _totalCell;
        //        }
        //    }
        //}
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
