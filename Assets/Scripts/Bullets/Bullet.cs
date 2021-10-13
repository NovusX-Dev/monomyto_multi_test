using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _bulletSpeed = 2f;
    [SerializeField] float _bulletPower = 0.5f;
    [SerializeField] Sprite _bulletSprite = null;

    private Vector3 _direction;
    private Vector3 _forwardDirection;

    Rigidbody2D _rb2d;
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Destroy(gameObject, 5f);
        if(_bulletSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = _bulletSprite;
        }
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rb2d.velocity = _forwardDirection * _bulletSpeed * Time.fixedDeltaTime;
    }

    public void SetBulletDirection(Vector2 direction)
    {
        _forwardDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<IDamagable<float>>() != null)
        {
            other.GetComponent<IDamagable<float>>().Damage(_bulletPower);
            Destroy(gameObject);
        }
    }
}
