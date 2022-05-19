using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    //TODO: fix isAttack
    [SerializeField] public float distanceAtack;
    [HideInInspector] public bool isAttack;
    [SerializeField] public int health;
    [SerializeField] public int damage;
    [SerializeField] public int speed;


    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.GetComponent<Enemy>() is Enemy || collision.GetComponent<Arrow>() is Arrow) && !isAttack)
        {
            ReciveDamage();
        }
            

    }
    private void ReciveDamage()
    {
        health--;
        if (health <= 0)
            Die();
        Debug.Log(health);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
