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
    public Hero hero;
    private Rigidbody2D _heroRigidbody2D;
    public bool isDragging { get; private set; }
    private bool isTouch;

    public void Awake()
    {
        hero = FindObjectOfType<Hero>();
        _heroRigidbody2D = hero.GetComponent<Rigidbody2D>();
        _directionDrag = Vector2.zero;
        isDragging = false;
    }
    private void Update()
    {
        if (_heroRigidbody2D.velocity.magnitude <= 1)
        {
            hero.isAttack = false;
            //_heroRigidbody2D.velocity = Vector2.zero;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        _directionTouch = Vector2.zero;
        isDragging = true;
        Vector2 curentPosition = eventData.position;
        Vector2 directionRaw = curentPosition - _origin;
        _directionDrag = directionRaw.normalized;
    }

    private void FixedUpdate()
    {
        if (isDragging)
            Moove(GetDirectionDrag());
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
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
        _directionTouch = Vector2.zero;
        _origin = eventData.position;
        _directionTouch =  Camera.main.ScreenToWorldPoint(Input.touches[0].position);

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        _directionDrag = Vector2.zero;
        if (!isDragging)
        {
            hero.isAttack = true;
            Attac(GetDirectionTouch());
        }
    }
    public void Moove(Vector2 dir)
    {
        Rotation(dir);
        _heroRigidbody2D.AddForce(dir * hero.speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }

    public void Attac(Vector3 dir)
    {
        var direction = dir - hero.transform.position;
        _heroRigidbody2D.AddForce(direction.normalized * hero.distanceAtack, ForceMode2D.Impulse);
    }

    private float _speedRotate = 5f;
    public int rotationOffset = -90;
    private float rotZ;

    private void Rotation(Vector3 dir)
    {
        Vector3 difference = dir - transform.position;
        rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(rotZ + rotationOffset, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * _speedRotate);
    }
}
