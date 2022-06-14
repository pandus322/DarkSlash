using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Soul : DropItem
{

    [SerializeField] private Wallet _wallet;
    [SerializeField] private float _speed;
    private void FixedUpdate()
    {
        if (Target._magnet)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, _speed * Time.deltaTime);
        }
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Hero>() is Hero)
        {
            _wallet.AddSoul();
            Destroy(gameObject);
        }
    }
}
