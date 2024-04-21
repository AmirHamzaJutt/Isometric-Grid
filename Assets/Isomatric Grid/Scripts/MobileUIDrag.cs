using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class MobileUIDrag : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool _IsDragging;
    private bool _IsTouch;
    public void OnDrag(PointerEventData data)
    {
        Debug.LogError("drag");
        _IsDragging = true;
    }
   
    public void OnEndDrag(PointerEventData data)
    {
        Debug.LogError("drag end");
        _IsDragging = false;
    }

    public bool OnDragState()
    {
        return _IsDragging;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.LogError("touch down");
        _IsTouch = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.LogError("touch up");
        _IsTouch = true;

    }
    public bool OnPointerState()
    {
        return _IsTouch;
    }
}
