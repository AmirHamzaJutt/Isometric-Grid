using UnityEngine;

namespace IsometricGrid.Data
{
    public class GridData : MonoBehaviour
    {
        public TileProperty[][] TerrainGrid { get; set; }
        public class TileProperty
        {
            public int TileType { get; set; }
        }
    }
}