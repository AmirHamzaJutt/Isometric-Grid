using IsometricGrid.GridTile;
using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace IsometricGrid.GridInputSystem
{
    public class Input_System : MonoBehaviour
    {
        public static Action RightMouseClick;
        public static Action LeftMouseClick;
        private Camera _mainCamera;
        [SerializeField] private LayerMask Layer;
        public Transform Target;
        private PlacmentManager _placmentManager;
        private void Start()
        {
            _placmentManager=GetComponent<PlacmentManager>();
            _mainCamera = GetComponent<Camera>();
        }
        private void Update()
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, Layer))
            {
                Vector3 targetPosition = raycastHit.collider.bounds.center;
                Target.transform.position = targetPosition;

                if (Input.GetMouseButtonDown(0))
                {
                    Tile tempTile = raycastHit.transform.gameObject.GetComponent<Tile>();
                    _placmentManager.PlaceObject(Target, tempTile);

                    //if (RightMouseClick != null)
                    //{
                    //    RightMouseClick();
                    //}
                }
                //if (Input.GetMouseButtonDown(1))
                //{
                //    if (LeftMouseClick != null)
                //    {
                //        LeftMouseClick();
                //    }
                //}
            }
          
        }
    }
}

