using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchPad : MonoBehaviour, IEndDragHandler, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    // —делать метод возвращающий координаты клика пр PointerDown
    private Vector2 _origin;
    private Vector2 _directionDrag;
    private Vector2 _directionTouch;
    public bool isDragging { get; private set; }
    public bool isOnPointerUp { get; private set; }

    public void OnDrag(PointerEventData eventData)
    {
        isOnPointerUp = false;
        _directionTouch = Vector2.zero;
        isDragging = true;
        Vector2 curentPosition = eventData.position;
        Vector2 directionRaw = curentPosition - _origin;
        _directionDrag = directionRaw.normalized;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        isOnPointerUp = false;
        _directionDrag = Vector2.zero;
    }

    private void Awake()
    {
        _directionDrag = Vector2.zero;
    }

    public Vector2 GetDirectionDrag()
    {
        return _directionDrag;
    }

    public Vector2 GetDirectionTouch()
    {
        return _directionTouch;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isOnPointerUp = false;
        _directionTouch = Vector2.zero;
        _origin = eventData.position;
        _directionTouch =  Camera.main.ScreenToWorldPoint(Input.touches[0].position);

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isOnPointerUp = true;
        _directionDrag = Vector2.zero;
        Invoke("ResetDirection", 0.06f);
    }

    private void ResetDirection()
    {
        isOnPointerUp = false;
        isDragging = false;
        _directionTouch = Vector2.zero;
    }
}
