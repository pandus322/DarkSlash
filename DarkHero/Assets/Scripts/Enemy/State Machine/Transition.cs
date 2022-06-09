using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    protected Hero Target { get; private set; }
    public State TargetState => _targetState;
    public bool NeedTransit { get; protected set; }


    public void Init(Hero target)
    {
        Target = target;
    }

    private void OnEnable()
    {
        NeedTransit = false;
    }
}

