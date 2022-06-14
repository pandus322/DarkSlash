using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _timeLife;
    private Rigidbody2D _rigidbody2D;
    private Hero _heroTarget;
    private Vector2 directionn;
    private void Awake()
    {
        _heroTarget = FindObjectOfType<Hero>();
    }
    void Start()
    {
        var target = FindObjectOfType<Hero>().transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        directionn = _heroTarget.transform.position - transform.position;
        Destroy(gameObject, _timeLife);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Hero>() is Hero && _heroTarget.isAttack)
        {
            Die();
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        Moove();
    }

    private void Moove()
    {
        _rigidbody2D.AddForce(directionn.normalized * _speed, ForceMode2D.Impulse);
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
