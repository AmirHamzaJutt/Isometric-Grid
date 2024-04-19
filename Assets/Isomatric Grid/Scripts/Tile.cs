using UnityEngine;

namespace IsometricGrid.GridTile
{
    public class Tile : MonoBehaviour
    {
        public int Id;
        public int TileType;
        public float TileSize;
        public bool IsOccupied;
        [SerializeField] private GameObject[] Texture;
        private void Start()
        {
            Texture[TileType].gameObject.SetActive(true);
            transform.localScale =new Vector3( TileSize,TileSize,TileSize);
        }
    }
}
