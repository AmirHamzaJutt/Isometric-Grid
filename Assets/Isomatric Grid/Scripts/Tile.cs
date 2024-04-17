using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsometricGrid.Tile
{
    public class Tile : MonoBehaviour
    {
        public int _ID;
        [SerializeField] private int _tileType;
        [SerializeField] private GameObject[] _texture;
        public int TileSize;
        private void Start()
        {
            _texture[_tileType].gameObject.SetActive(true);
        }
    }
}
