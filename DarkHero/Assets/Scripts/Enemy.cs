using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int _attackVelocity;
    public float _attackRange;
    public int speed;
    public int health;
    public int damage;



    public bool isShooter;


    [HideInInspector]public Hero targetHero;

    private void Awake()
    {
        targetHero = FindObjectOfType<Hero>();

    }

    public void ReciveDamage(int doneDamage)
    {
        health -= doneDamage;
        if (health <= 0)
            Die();
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    //Trigger Damage to Enemy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.GetComponent<Hero>() is Hero && targetHero.isAttack)
        {
            ReciveDamage(targetHero.damage);
        }

    }
}
