using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable<float>
{
    public static event Action<int> OnEmenyKilledPoints;

    [SerializeField] float _maxHealth = 20f;
    [SerializeField] int _scorePoints = 30;


    private float _currentHealth;

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

        if( _currentHealth < 0 )
        {
            OnEmenyKilledPoints.Invoke(_scorePoints);
            Destroy(gameObject);
        }
        
    }
}
