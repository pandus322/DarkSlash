using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Hero : MonoBehaviour
{
    //TODO: fix isAttack
    [SerializeField] public float distanceAtack;
    [SerializeField] public bool isAttack;
    [SerializeField] public int health;
    [SerializeField] public int damage;
    [SerializeField] public int speed;
    [SerializeField] private Heart HeartUI;


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
        HeartUI.RemooveHeart();
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
