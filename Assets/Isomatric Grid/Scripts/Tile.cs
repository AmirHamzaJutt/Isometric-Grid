using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IsometricGrid.Tile
{
    public class Tile : MonoBehaviour
    {
        public int _ID;
        public int TileType;
        [SerializeField] private GameObject[] _texture;
        public int TileSize;
        private void Start()
        {
            _texture[TileType].gameObject.SetActive(true);
        }
    }
}
