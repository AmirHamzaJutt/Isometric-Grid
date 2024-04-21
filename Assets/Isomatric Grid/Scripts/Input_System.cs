using IsometricGrid.GridTile;
using IsometricGrid.PlacmentController;
using UnityEngine;
using UnityEngine.UI;

namespace IsometricGrid.GridInputSystem
{
    public class Input_System : MonoBehaviour
    {
        [SerializeField] private Button Reset;
        [SerializeField] private LayerMask Layer;
        [SerializeField] Type.TileType Tile_type = Type.TileType.Wood;


        public Highliter HighliterPointer;
        public GridMaker.GenerateGrid GenratedGrid;

        private CustomGrid.Grid _grid;
        private Camera _mainCamera;
        private PlacmentManager _placmentManager;

        private void Start()
        {
            Reset.onClick.AddListener(() => { ResetGrid(); });
            if (GetComponent<PlacmentManager>() != null)
            {
                _placmentManager = GetComponent<PlacmentManager>();
                _placmentManager.SetGrid(GenratedGrid);
            }
            _mainCamera = GetComponent<Camera>();
            Invoke(nameof(SetGrid), 0.2f);
        }
        public void SetGrid()
        {
            _grid = GenratedGrid.GetGrid();
        }
        public void ResetGrid()
        {
            _placmentManager.ResetSpawnObject();
        }
        private void OnMouseDown()
        {
            Debug.LogError("mouse click");
        }
        private void Update()
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, Layer))
            {
                Vector3 targetPosition = raycastHit.collider.bounds.center;
                HighliterPointer.transform.position = targetPosition;
                // Tile tempTile = raycastHit.transform.gameObject.GetComponent<Tile>();tempTile.TileType == (int)Tile_type
                int tileType = _grid.GetType(targetPosition);
                bool occupiedCheck = _grid.GetOccupiedCell(targetPosition);
                HighliterPointer.SetColor(!occupiedCheck, tileType == (int)Tile_type);
                //if (!tempTile.IsOccupied && tempTile.TileType == (int)TileType)
                //{
                //}
                //else
                //{
                //    Debug.LogError("Can't able to placed Object");
                //}

                if (tileType == (int)Tile_type && !occupiedCheck)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        _placmentManager.PlaceObject(HighliterPointer.transform.position, (int)Tile_type);
                    }
                }
                else
                {
                    Debug.LogError("Area Not Available");
                }
            }
        }
    }
}

