using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable<float>
{
    public static event Action<int> OnEmenyKilledPoints;

    [SerializeField] float _maxHealth = 20f;
    [SerializeField] int _scorePoints = 30;
    [SerializeField] ParticleSystem _deathVFX;

    private float _currentHealth;
    private bool _isDead;

    public bool IsDead => _isDead;

    Animator _animController;

    private void Awake()
    {
        _animController = GetComponent<Animator>();
    }

    void Start()
    {
        _currentHealth = _maxHealth;
    }

    void Update()
    {
        
    }

    public void Damage(float amount)
    {
        _currentHealth -= amount;
        _animController.SetTrigger("damaged");


        if( _currentHealth < 0 )
        {
            OnEmenyKilledPoints.Invoke(_scorePoints);
            _isDead = true;
             GetComponent<Collider2D>().enabled = false;
            _animController.SetBool("isDead", _isDead);
        }
    }

    public void DeathEvent()
    {
        Instantiate(_deathVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
