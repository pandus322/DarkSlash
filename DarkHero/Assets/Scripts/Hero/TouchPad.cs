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
    private float _offRotation = 90f;
    public bool isDragging { get; private set; }
    private bool isTouch;
    [SerializeField] private Animator _animator;

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
            _animator.ResetTrigger("isAttack");

            hero.isAttack = false;
            _heroRigidbody2D.rotation = 0;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        _directionTouch = Vector2.zero;
        isDragging = true;
        Vector2 curentPosition = eventData.position;
        Vector2 directionRaw = curentPosition - _origin;
        _directionDrag = directionRaw;
    }

    private void FixedUpdate()
    {
        if (isDragging)
            Moove(GetDirectionDrag());

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        _animator.ResetTrigger("RunX");
        _animator.ResetTrigger("RunY");
        _animator.SetTrigger("NoRun");
        hero.GetComponent<SpriteRenderer>().flipY = false;
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
        if (dir.y > 0)
        {
            hero.GetComponent<SpriteRenderer>().flipY = true;
            _animator.ResetTrigger("RunX");
            _animator.SetTrigger("RunY");

        }
        else if (dir.y < 0)
        {
            hero.GetComponent<SpriteRenderer>().flipY = false;
            _animator.ResetTrigger("RunY");
            _animator.SetTrigger("RunX");
        }

        var lookDir = dir - _heroRigidbody2D.position;
        float angel = Mathf.Atan2(lookDir.normalized.y, lookDir.normalized.x) * Mathf.Rad2Deg + _offRotation;
        _heroRigidbody2D.rotation = angel;
        dir = dir.normalized;
        _heroRigidbody2D.MovePosition(_heroRigidbody2D.position + dir * hero.Speed * Time.fixedDeltaTime);

    }

    public void Attac(Vector3 dir)
    {
        _animator.SetTrigger("isAttack");

        var lookDir = GetDirectionTouch() - _heroRigidbody2D.position;
        float angel = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + _offRotation;
        _heroRigidbody2D.rotation = angel;

        var direction = dir - hero.transform.position;
        direction = direction.normalized;

        var powerAttack = CheckDistanceAttack(hero.PowerAttack, direction);
        _heroRigidbody2D.AddForce(direction * powerAttack, ForceMode2D.Impulse);
    }

    private float CheckDistanceAttack(float powerAttack, Vector2 direction)
    {

        for (int i = 0; i < hero.DistanceAtack; i++)
        {
            Vector2 vectorAttack = direction * powerAttack;
            if (vectorAttack.magnitude < 20)
            {
                powerAttack +=250;
            }
            else if (vectorAttack.magnitude > hero.DistanceAtack+5)
            {
                powerAttack -= 100;

            }
        }
        return powerAttack;
    }
}
