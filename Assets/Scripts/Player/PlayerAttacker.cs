using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] InputAction _normalAttack;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _weaponPos;
    
    //fire rate will be assigned by the weapon later, remove serialization
    [SerializeField] private float _fireRate = 0.25f;
    private float _nextFire = -1;

    private void OnEnable()
    {
        _normalAttack.Enable();
    }

    private void OnDisable()
    {
        _normalAttack.Disable();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(_normalAttack.ReadValue<float>() > 0.5f && Time.time > _nextFire)
        {
            var bullet = Instantiate(_bulletPrefab, _weaponPos.position, Quaternion.identity);
            bullet.GetComponent<Bullet>().SetBulletDirection(transform.right);
            _nextFire = Time.time + _fireRate;
        }
    }
}
