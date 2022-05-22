using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    //TODO: fix isAttack
    [SerializeField] public float distanceAtack;
    [SerializeField] public bool isAttack;
    [SerializeField] public int health;
    [SerializeField] public int damage;
    [SerializeField] public int speed;


    void Update()
    {
      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.GetComponent<Enemy>() is Enemy || collision.GetComponent<Projectile>() is Projectile) && !isAttack)
        {
            ReciveDamage();
            if(collision.GetComponent<Projectile>() != null)
            {
                collision.GetComponent<Projectile>().Die();
            }
        }
    }
    public void ReciveDamage()
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
