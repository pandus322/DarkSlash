using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActionController : MonoBehaviour
{
    private Enemy _thisEnemy;
    private Rigidbody2D _rigidbody2D;

    private float _distanceToPlayer;
    private bool _isAttack;

    private float _timePreparationAttack;
    [SerializeField]private float _StartTimePreparationAttack;

    [SerializeField]private GameObject projectile;
    private float _timeBtwShots;
    [SerializeField]private float _StartTimeBtwShots;
    [SerializeField] private GameObject _pointAttack;

    [SerializeField] private Animator _animator;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _thisEnemy = GetComponent<Enemy>();
        DistanceCalculation();
        _isAttack = false;
        _timePreparationAttack = _StartTimePreparationAttack;
        _timeBtwShots = _StartTimeBtwShots;
    }
    
    private void Update()
    {
        DistanceCalculation();
        if (_rigidbody2D.velocity.magnitude <= 1)
        {
            _thisEnemy.IsAttack = false;
            if (!_thisEnemy.isShooter)
                _animator.ResetTrigger("IsAttack");
            _rigidbody2D.rotation = 0;
        }

        if (transform.position.x < _thisEnemy.Target.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if(transform.position.x > _thisEnemy.Target.transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;

        }
    }

    private void Move(int speed)
    {
        var direction = _thisEnemy.Target.transform.position - transform.position;
        _rigidbody2D.AddForce(direction.normalized * speed * Time.deltaTime, ForceMode2D.Impulse);
    }
    private void DistanceCalculation()
    {
        _distanceToPlayer = Vector2.Distance(transform.position, _thisEnemy.Target.transform.position);
    }

    private void FixedUpdate()
    {
        if (_thisEnemy.isShooter)
            _pointAttack.transform.rotation = Rotation();

        if (_distanceToPlayer > _thisEnemy._attackRange && !_isAttack)
        {
            _animator.SetTrigger("IsMoove");
            Move(_thisEnemy.speed);
        }
        else if(_distanceToPlayer < _thisEnemy._attackRange)
        {
            if (_timeBtwShots <= 0)
            {
                _animator.ResetTrigger("IsMoove");
                _animator.SetTrigger("IsPrepairAttack");

                _isAttack = true;
            }
        }
        if (_timePreparationAttack <= 0)
        {
            _isAttack = false;
            _animator.ResetTrigger("IsPrepairAttack");
            if(!_thisEnemy.isShooter)
                _animator.SetTrigger("IsAttack");
            Attack();
        }

        if (_isAttack)
            _timePreparationAttack -= Time.deltaTime;


        if(!_isAttack)
            _timeBtwShots -= Time.deltaTime;
    }

    private void Attack()
    {
        if (!_thisEnemy.isShooter)
        {
            _thisEnemy.IsAttack = true;
            var direction = _thisEnemy.Target.transform.position - transform.position;
            var powerAttack = CheckDistanceAttack(_thisEnemy._attackVelocity, direction.normalized);
            transform.rotation = Rotation();
            _rigidbody2D.AddForce(direction.normalized * powerAttack, ForceMode2D.Impulse);
        }
        else if (_thisEnemy.isShooter)
        {
            Instantiate(projectile, _pointAttack.transform.position, _pointAttack.transform.rotation);
        }

        _timeBtwShots = _StartTimeBtwShots;
        _timePreparationAttack = _StartTimePreparationAttack;

    }

    private float CheckDistanceAttack(float powerAttack, Vector2 dir)
    {

        for (int i = 0; i < 20; i++)
        {
            Vector2 qw = dir * powerAttack;
            if (qw.magnitude < 15)
            {
                powerAttack += 250;
            }
            else if (qw.magnitude > 20)
            {
                powerAttack -= 100;

            }
        }
        return powerAttack;
    }


    public int rotationOffset;
    private Quaternion Rotation()
    {
        Vector3 difference = _thisEnemy.Target.transform.position - transform.position;
        var rotZ = Mathf.Atan2 (difference.y, difference.x)* Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(rotZ + rotationOffset, Vector3.forward);
        return rotation;
    }
}
