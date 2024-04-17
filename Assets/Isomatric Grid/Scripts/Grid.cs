using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsometricGrid.Grid
{
    public class Grid : MonoBehaviour
    {
        private int _width;
        private int _height;
        private float _cellSize;
        private int[,] _gridArray;
        public Grid(int width, int height, float cellSize)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _gridArray = new int[width, height];
            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArray.GetLength(1); y++)
                {
                   // Debug.LogError(x + " " + y);
                    Debug.DrawLine(GetWorlPosition(x, y), GetWorlPosition(x, y + 1),Color.black,100f);
                    Debug.DrawLine(GetWorlPosition(x, y), GetWorlPosition(x=1, y), Color.red,100f);
                }
            }
        }
        private Vector3 GetWorlPosition(int x,int y)
        {
            return new Vector3(x, y) * _cellSize;
        }
    }
}
