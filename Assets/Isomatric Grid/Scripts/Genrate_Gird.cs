using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsometricGrid.GenerateGrid
{
    public class GenerateGrid : MonoBehaviour
    {
        [SerializeField] private Tile.Tile _tilePrefab;
        [SerializeField] private int _row;
        [SerializeField] private int _column;
        [SerializeField] private float _tileSize;
        [SerializeField] private int _totalCell;
        void Start()
        {
            Grid.Grid grid =new Grid.Grid(5,5,1);
            _tileSize = _tilePrefab.TileSize;
            DrawGrid();
        }
        void DrawGrid()
        {
            for (int i = 0; i < _row; i++)
            {
                Vector3 offsetZ = new Vector3(0, 0, _tileSize) * i;

                for (int j = 0; j < _column; j++)
                {
                    Tile.Tile tempTile = Instantiate(_tilePrefab, transform.position, transform.rotation);
                    tempTile.transform.parent = transform;
                    Vector3 offsetX = new Vector3(_tileSize, 0, 0) * j;
                    tempTile.transform.localPosition = offsetX + offsetZ;
                    _totalCell += 1;
                    tempTile._ID = _totalCell;
                }
            }
        }

    }
}
