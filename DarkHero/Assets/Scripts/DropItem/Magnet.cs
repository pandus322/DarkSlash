using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : DropItem
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Hero>() is Hero)
        {
            Target.ActivateMagnet();
            Destroy(gameObject);

        }
    }
}
