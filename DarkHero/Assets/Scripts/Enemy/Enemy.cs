using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public bool IsPrepairAttack;
    public int _attackVelocity;
    public float _attackRange;
    public int speed;
    public int health;
    public int damage;
    public int chanceDrop;
    public GameObject drop;

    public event UnityAction<Enemy> Dying;



    public bool isShooter;
    [SerializeField] private Hero _target;
    public Hero Target => _target;

    private void Awake()
    {
        _target = FindObjectOfType<Hero>();

    }

    public void Init(Hero target)
    {
        _target = target;
    }

    public void ReciveDamage(int doneDamage)
    {
        health -= doneDamage;
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Dying?.Invoke(this);
        Drop();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.GetComponent<Hero>() is Hero && _target.isAttack)
        {
            ReciveDamage(_target.damage);
        }
    }

    private void Drop()
    {
        int chance = Random.Range(0, 31);
        if(chance < chanceDrop)
        {
            Instantiate(drop, transform.position, Quaternion.identity);
        }
    }
}
