using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropItem : MonoBehaviour
{
    [SerializeField] Collider2D _colider2D;
    [SerializeField] private float _timeActivateColider;
    protected Hero Target;


    public void Init(Hero target)
    {
        Target = target;
    }

    private void Update()
    {
        _timeActivateColider -= Time.deltaTime;
        if (_timeActivateColider <= 0)
            _colider2D.isTrigger = true;
    }
    protected abstract void OnTriggerEnter2D(Collider2D collision);
}
