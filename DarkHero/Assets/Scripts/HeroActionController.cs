using UnityEngine;

public class HeroActionController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private TouchPad _touchPad;
    private Hero _thisHero;

    private void Awake()
    {
        _touchPad = FindObjectOfType<TouchPad>();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _thisHero = GetComponent<Hero>();
    }


    private void FixedUpdate()
    {
        if (_touchPad.isDragging && !_touchPad.isOnPointerUp)
        {
            _thisHero.isAttack = false;
            Moove(_touchPad.GetDirectionDrag());
        }

        if (_touchPad.isOnPointerUp && !_touchPad.isDragging && !_thisHero.isAttack)
        {
            _thisHero.isAttack = true;
            Attac(_touchPad.GetDirectionTouch());
        }
    }

    private void Moove(Vector2 dir)
    {
        _rigidbody.AddForce(dir * _thisHero.speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    private void Attac(Vector3 dir)
    {
        //transform.position = Vector2.MoveTowards(transform.position, dir, _thisHero.distanceAtack * Time.deltaTime);

        var direction = dir - transform.position;
        _rigidbody.AddForce(direction.normalized * _thisHero.distanceAtack * Time.deltaTime, ForceMode2D.Impulse);
        _thisHero.isAttack = false;

    }
}
