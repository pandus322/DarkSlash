using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrepairAttackTransition : Transition
{
    [SerializeField] private float _startTimePreparationAttack;
    private float _timePreparationAttack;
    private void Start()
    {
        _timePreparationAttack = _startTimePreparationAttack;
    }

    private void Update()
    {
        _timePreparationAttack -= Time.deltaTime;
        if (_timePreparationAttack <= 0)
        {
            _timePreparationAttack = _startTimePreparationAttack;
            NeedTransit = true;
        }
    }


}
