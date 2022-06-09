using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBetweenAttackTransition : Transition
{
    [SerializeField] private float _startTimeBetweenAttack;
    private float _timeBetweenAttack;

    private void Start()
    {
        _timeBetweenAttack = _startTimeBetweenAttack;
    }
    private void Update()
    {
        _timeBetweenAttack-=Time.deltaTime;
        if (_timeBetweenAttack <= 0)
        {
            NeedTransit = true;
            _timeBetweenAttack = _startTimeBetweenAttack;
        }
    }
}
