using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public bool IsAttack;
    public int _attackVelocity;
    public float _attackRange;
    public int speed;
    public int health;
    public int damage;
    public int chanceDrop;
    [SerializeField] private List<DropGoods> _goodsList;
    [SerializeField] private List<int> _chanceDropList;
    public event UnityAction<Enemy> Dying;
    [SerializeField] private GameObject _bloodParticle;
    public bool isDead;



    public bool isShooter;
    [SerializeField] private Hero _target;
    public Hero Target => _target;

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
        GetComponent<EnemyActionController>().enabled = false;
        isDead = true;
        Dying?.Invoke(this);
        GetComponent<Animator>().SetTrigger("IsDie");
        DropChance();
        Destroy(Instantiate(_bloodParticle, transform), 1);
        Destroy(gameObject,2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.GetComponent<Hero>() is Hero && _target.isAttack)
        {
            ReciveDamage(_target.Damage);
        }
    }

    private void DropChance()
    {
        int chance = Random.Range(0, 10*_chanceDropList.Count+11);
        for (int i = 0; i < _chanceDropList.Count; i++)
        {
            if (i == 0)
            {
                if (chance <= _chanceDropList[i])
                {
                    if(_goodsList[i].Level>0)
                        Drop(_goodsList[i].DropTamplate);
                }
            }
            else if(chance > _chanceDropList[i-1] && chance <= _chanceDropList[i])
            {
                if (_goodsList[i].Level > 0)
                    Drop(_goodsList[i].DropTamplate);
            }
        }
    }

    private void Drop(GameObject dropTamplate)
    {
        var drop = Instantiate(dropTamplate, transform.position, Quaternion.identity).GetComponent<DropItem>();
        drop.Init(_target);
    }
}
