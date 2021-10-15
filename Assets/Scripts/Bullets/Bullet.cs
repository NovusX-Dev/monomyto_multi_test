using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _bulletSpeed = 2f;
    [SerializeField] float _bulletPower = 0.5f;
    [Tooltip("Must be equal to a pool manager ID")] [SerializeField] int _bulletID;
    [SerializeField] Sprite _bulletSprite = null;

    private Vector3 _forwardDirection;

    Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke("Hide", 4f);
    }

    void Start()
    {
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
            gameObject.SetActive(false);;
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public int GetBulletID()
    {
        return _bulletID;
    }
}
