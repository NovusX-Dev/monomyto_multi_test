using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] BulletSO _bulletSO;

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
        if (_bulletSO == null) Debug.LogError("Bullet Scriptable Object is not assigned!");
        _bulletSO.SetTagString(this.tag);

        if (_bulletSO.bulletSprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = _bulletSO.bulletSprite;
        }
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _rb2d.velocity = _forwardDirection * _bulletSO.bulletSpeed * Time.fixedDeltaTime;
    }

    public void SetBulletDirection(Vector2 direction)
    {
        _forwardDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<IDamagable<float>>() != null)
        {
            other.GetComponent<IDamagable<float>>().Damage(_bulletSO.bulletPower);
            gameObject.SetActive(false);;
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    public string GetBulletTag()
    {
        return this.tag;
    }
}
