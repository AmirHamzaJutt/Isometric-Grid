using UnityEngine;
using UnityEngine.EventSystems;

public class MobileUIDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Transform ObjectToMove;
    public Vector2 DragLimit = new Vector2(2f, 2f);
    public float SmoothTime = 0.05f;

    private bool _isDragging = false;
    private bool _isclicked = false;
    private Vector3 _originalPosition;
    private int _pointerId;
    private Vector3 _dragStartPosition;
    private Vector3 _dragStartObjectPosition;


    private Vector3 _velocity = Vector3.zero;

    private void Start()
    {
        _originalPosition = ObjectToMove.position;
    }

    public void OnDrag(PointerEventData data)
    {
        _isDragging = true;

        if (data.pointerId == _pointerId)
        {
            Vector3 currentPosition = data.position;
            Vector3 displacement = currentPosition - _dragStartPosition;
            displacement *= -1;
            Vector3 newPosition = _dragStartObjectPosition + ClampMovement(displacement);
            ObjectToMove.position = Vector3.SmoothDamp(ObjectToMove.position, newPosition, ref _velocity, SmoothTime);
        }
    }

    public void OnEndDrag(PointerEventData data)
    {
        _isDragging = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isclicked = false;
        _pointerId = eventData.pointerId;
        _dragStartPosition = eventData.position;
        _dragStartObjectPosition = ObjectToMove.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isclicked=true;
    }

    private Vector3 ClampMovement(Vector3 displacement)
    {
        Vector3 newPosition = ObjectToMove.position + displacement;
        newPosition.x = Mathf.Clamp(newPosition.x, _originalPosition.x - DragLimit.x, _originalPosition.x + DragLimit.x);
        newPosition.y = Mathf.Clamp(newPosition.y, _originalPosition.y - DragLimit.y, _originalPosition.y + DragLimit.y);
        return newPosition - ObjectToMove.position;
    }

    public bool OnDragState()
    {
        return _isDragging;
    }

    public bool OnPointerState()
    {
        return _isclicked;
    }
}
