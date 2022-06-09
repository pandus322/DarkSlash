using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTranstion : Transition
{
    private Enemy _thisEnemy;
    private void Start()
    {
        _thisEnemy = GetComponent<Enemy>();
    }
    private void Update()
    {
        if (_thisEnemy.IsPrepairAttack)
        {
            _thisEnemy.IsPrepairAttack = false;
            NeedTransit = true;
        }
    }
}
