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
        Rotation();

        if (_distanceToPlayer > _thisEnemy._attackRange && !_isAttack)
        {
            Move(_thisEnemy.speed);
        }
        else if(_distanceToPlayer < _thisEnemy._attackRange)
        {
            if (_timeBtwShots <= 0)
            {
                _isAttack = true;
            }
        }
        if (_timePreparationAttack <= 0)
        {
            _isAttack = false;
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
            var direction = _thisEnemy.Target.transform.position - transform.position;
            _rigidbody2D.AddForce(direction.normalized * _thisEnemy._attackVelocity, ForceMode2D.Impulse);
        }
        else if (_thisEnemy.isShooter)
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }

        _timeBtwShots = _StartTimeBtwShots;
        _timePreparationAttack = _StartTimePreparationAttack;

    }


    private float _speedRotate = 5f;
    public int rotationOffset = -90;
    private float rotZ;

    private void Rotation()
    {
        Vector3 difference = _thisEnemy.Target.transform.position - transform.position;
        rotZ = Mathf.Atan2 (difference.y, difference.x)* Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(rotZ + rotationOffset, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime* _speedRotate);
    }
}
