using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAtackState : State
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _attackVelocity;

    private Enemy _thisEnemy;
    private void Start()
    {
        _thisEnemy = GetComponent<Enemy>();
    }

    private void FixedUpdate()
    {
        if (!_thisEnemy.IsPrepairAttack)
        {
            var direction = Target.transform.position - transform.position;
            _rigidbody2D.AddForce(direction.normalized * _attackVelocity, ForceMode2D.Impulse);
            _thisEnemy.IsPrepairAttack = true;
        }

    }
}
