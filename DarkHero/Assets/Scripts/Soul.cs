using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Soul : MonoBehaviour
{

    [SerializeField] private Wallet _wallet;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.GetComponent<Hero>() is Hero && !collision.GetComponent<Hero>().isAttack)
        {
            _wallet.AddSoul();
            Destroy(gameObject);
        }
    }
}
