using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour, IDamagable<float>
{
    [SerializeField] float _maxHP = 1f;

    private float _currentHP;

    private void Start()
    {
        _currentHP = _maxHP;
    }

    private void Update()
    {
        
    }


    void IDamagable<float>.Damage(float damageTaken)
    {
        _currentHP -= damageTaken;

        if (_currentHP <= 0)
        {
            Destroy(gameObject);
        }
    }
}
