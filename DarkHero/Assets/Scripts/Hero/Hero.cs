using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Hero : MonoBehaviour
{
    [SerializeField] public float DistanceAtack;
    [SerializeField] public float PowerAttack;
    [SerializeField] public bool isAttack;
    [SerializeField] public int Health;
    [SerializeField] public int Damage;
    [SerializeField] public int Speed;
    [SerializeField] private Heart HeartUI;
    [SerializeField] private GameObject _shieldTemplate;
    [SerializeField] private GameObject _magnetTemplate;

    private float _timeActivSield;
    [SerializeField] private float _startTimeActiveShield;
    public bool _shield;

    private float _timeActiveMagnet;
    [SerializeField] private float _startTimeActiveMagnet;
    public bool _magnet;

    public event UnityAction Dying;

    public void Init(int healthLevel, int damageLevel, int agilityLevel)
    {
        Health += healthLevel;
        Damage += damageLevel;
        Speed += agilityLevel;
        DistanceAtack += agilityLevel * 5;
    }

    private void Update()
    {
        if (_magnet)
        {
            _timeActiveMagnet -=Time.deltaTime;
        }
        if (_timeActiveMagnet<=0)
        {
            _magnet = false;
            _timeActiveMagnet = _startTimeActiveMagnet;

            
        }        
        if (_shield)
        {
            _timeActivSield -=Time.deltaTime;
        }
        if (_timeActivSield<=0)
        {
            _shield = false;
            _timeActivSield = _startTimeActiveShield;
        }
    }
    public void ActivateMagnet()
    {
        Destroy(Instantiate(_magnetTemplate, transform), _startTimeActiveMagnet);
        _magnet = true;
    }

    public void ActicateShield()
    {
        Destroy(Instantiate(_shieldTemplate, transform), _startTimeActiveShield);

        _shield = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.GetComponent<Enemy>() is Enemy || collision.GetComponent<Projectile>() is Projectile) && !isAttack)
        {
            if(collision.GetComponent<Enemy>() != null)
                if(!collision.GetComponent<Enemy>().isDead && collision.GetComponent<Enemy>().IsAttack)
                    ReciveDamage();

            if (collision.GetComponent<Projectile>() != null)
            {
                ReciveDamage();
                collision.GetComponent<Projectile>().Die();
            }
        }
    }
    public void ReciveDamage()
    {
        HeartUI.RemooveHeart();
        Health--;
        if (Health <= 0)
        {
            Dying?.Invoke();
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
