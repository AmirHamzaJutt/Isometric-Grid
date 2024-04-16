using UnityEngine;

public class Input_System : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Transform _inaccuracy;
    [SerializeField] GameObject _snapEffect;
    [SerializeField] private float _movespeed = 5;
    [SerializeField] private bool _snap;
    [Space(10)]
    [SerializeField] private float _offsetX = 1.0f; // Offset amount for X axis
    [SerializeField] private float _offsetY = 1.0f;
    [SerializeField] private float _offsetZ = 1.0f;
    private Vector3 targetPosition;
    Vector3 screenCenter;

    private void Start()
    {
    }
    private void Update()
    {
       // screenCenter = gameController.uiManager.Cursor().position;
        Ray ray = _mainCamera.ScreenPointToRay(screenCenter);
        //Debug.DrawRay(ray.origin, ray.direction * 5000f, Color.red);
        RaycastHit raycastHit;
        if (Physics.Raycast(ray, out raycastHit, 5000f, _layer))
        {
            if (raycastHit.collider.CompareTag("snap") && _snap)
            {
                Debug.LogError("snap");
                targetPosition = raycastHit.collider.bounds.center;
                _snapEffect.SetActive(true);
            }
            else
            {
                targetPosition = new Vector3(raycastHit.point.x + _offsetX, raycastHit.point.y + _offsetY, raycastHit.point.z + _offsetZ);
                _snapEffect.SetActive(false);
            }
            transform.position = Vector3.Lerp(transform.position, targetPosition, _movespeed * Time.deltaTime * 5f);
        }
    }
}
