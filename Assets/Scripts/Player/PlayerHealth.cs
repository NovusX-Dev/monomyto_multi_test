using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable<float>
{
    [SerializeField] float _maxHealth = 10f;

    private float _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
        UIManager.Instance.UpdateHealthUI(_currentHealth, _maxHealth);
    }

    void Update()
    {
        
    }

    public void Damage(float amount)
    {
        _currentHealth -= amount;

        if (_currentHealth < 0)
        {
            _currentHealth = 0f;
            Destroy(gameObject);
        }

        UIManager.Instance.UpdateHealthUI(_currentHealth, _maxHealth);
    }
}
