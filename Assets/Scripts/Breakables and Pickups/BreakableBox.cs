using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour, IDamagable<float>
{
    [SerializeField] float _maxHP = 1f;
    [SerializeField] ParticleSystem _explosionVFX;
    [SerializeField] GameObject[] _pickupPrefabs;

    private float _currentHP;

    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
            BreakBox();
        }
    }

    private void BreakBox()
    {
        var explosion = Instantiate(_explosionVFX, transform.position, Quaternion.identity);
        explosion.startColor = _spriteRenderer.color;

        Instantiate(_pickupPrefabs[Random.Range(0, _pickupPrefabs.Length)], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
