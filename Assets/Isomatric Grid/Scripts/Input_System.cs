using System;
using UnityEngine;

namespace IsometricGrid.InputSystem
{
    public class Input_System : MonoBehaviour
    {
        public static Action RightMouseClick;
        public static Action LeftMouseClick;

        private Camera _mainCamera;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private Transform Target;

        private void Start()
        {
            _mainCamera = GetComponent<Camera>();
        }
        private void Update()
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, _layer))
            {
                Vector3 targetPosition = raycastHit.point;
               Target. transform.position = targetPosition;
            }

            if (Input.GetMouseButtonDown(0))
            {
              //  
                if (RightMouseClick != null)
                {
                    RightMouseClick();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                // 
                if (LeftMouseClick != null)
                {
                    LeftMouseClick();
                }
            }
        }
        private void OnDrawGizmos()
        {
            if (_mainCamera != null)
            {
                Vector3 mousePosition = Input.mousePosition;
                Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
                Gizmos.color = Color.red;
                Gizmos.DrawRay(ray.origin, ray.direction * 500f);
            }
        }
       
    }
}

