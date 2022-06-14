using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : DropItem
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Hero>() is Hero)
        {
            Target.ActicateShield();
            Destroy(gameObject);

        }
    }
}
