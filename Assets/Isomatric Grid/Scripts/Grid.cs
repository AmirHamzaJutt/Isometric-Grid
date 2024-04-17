using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace IsometricGrid.Grid
{
    public class Grid : MonoBehaviour
    {
        private int _width;
        private int _height;
        private float _cellSize;
        private int[,] _gridArray;
        private TextMesh[,] _debugText;
        public Grid(int width, int height, float cellSize)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _gridArray = new int[width, height];
            _debugText=new TextMesh[width, height];
            for (int x = 0; x < _gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < _gridArray.GetLength(1); y++)
                {
                    // Debug.LogError(x + " " + y);
                    _debugText[x, y].text = _gridArray[x, y].ToString();
                    Debug.DrawLine(GetWorlPosition(x, y), GetWorlPosition(x+1, y), Color.red,100f);
                    Debug.DrawLine(GetWorlPosition(x, y), GetWorlPosition(x, y + 1),Color.black,100f);
                }
            }
            Debug.DrawLine(GetWorlPosition(0, height), GetWorlPosition(width,height), Color.red, 100f);
            Debug.DrawLine(GetWorlPosition(width, 0), GetWorlPosition(width ,height), Color.black, 100f);
            SetValue(2, 1, 56);
        }
        private Vector3 GetWorlPosition(int x,int y)
        {
            return new Vector3(x, y) * _cellSize;
        }
        public void SetValue(int x, int y, int value)
        {
            if(x>0&&y>=0&&x<_width && y<_height)
            {
                _gridArray[x,y] = value;
                _debugText[x, y].text = _gridArray[x,y].ToString();
            }
        }

    }
}
