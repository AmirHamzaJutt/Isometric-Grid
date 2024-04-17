using UnityEngine;

namespace IsometricGrid.InputSystem
{
    public class Input_System : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LayerMask _layer;
        private void Update()
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, _layer))
            {
                Vector3 targetPosition = raycastHit.point;
                transform.position = targetPosition;
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

