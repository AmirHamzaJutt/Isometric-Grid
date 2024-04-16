using UnityEngine;

public class Input_System : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _offsetX = 1f;
    [SerializeField] private float _offsetY = 1f;
    [SerializeField] private float _offsetZ = 1f;

    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Ray ray = _mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity, _layer))
        {
            Vector3 targetPosition = new Vector3(raycastHit.point.x + _offsetX, raycastHit.point.y + _offsetY, raycastHit.point.z + _offsetZ);
            transform.position = Vector3.Lerp(transform.position, targetPosition, _moveSpeed * Time.deltaTime);
        }
    }
    private void OnDrawGizmos()
    {
        // Draw the raycast in the scene view
        if (_mainCamera != null)
        {
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = _mainCamera.ScreenPointToRay(mousePosition);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(ray.origin, ray.direction * 5000f);
        }
    }
}

