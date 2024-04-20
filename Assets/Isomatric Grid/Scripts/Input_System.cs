using IsometricGrid.GridTile;
using IsometricGrid.PlacmentController;
using UnityEngine;
using UnityEngine.UI;

namespace IsometricGrid.GridInputSystem
{
    public class Input_System : MonoBehaviour
    {
        [SerializeField] private Button Reset;

        private enum _type { Dirt = 0, Grass = 1, Stone = 2, Wood = 3 }
        [SerializeField] private _type TileType = _type.Wood;

        public Highliter HighliterPointer;
        public GridMaker.GenerateGrid Grid;
        [SerializeField] private LayerMask Layer;

        private Camera _mainCamera;
        private PlacmentManager _placmentManager;

        private void Start()
        {
            Reset.onClick.AddListener(() => { ResetGrid(); });
            if (GetComponent<PlacmentManager>() != null)
            {
                _placmentManager = GetComponent<PlacmentManager>();
                _placmentManager.SetGrid(Grid);
            }
            _mainCamera = GetComponent<Camera>();
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
                Tile tempTile = raycastHit.transform.gameObject.GetComponent<Tile>();
                HighliterPointer.SetColor(!tempTile.IsOccupied, tempTile.TileType == (int)TileType);
                //if (!tempTile.IsOccupied && tempTile.TileType == (int)TileType)
                //{
                //}
                //else
                //{
                //    Debug.LogError("Can't able to placed Object");
                //}

                if (Input.GetMouseButtonDown(0))
                {
                  _placmentManager.PlaceObject(HighliterPointer.transform.position);
                }
            }
        }
    }
}

