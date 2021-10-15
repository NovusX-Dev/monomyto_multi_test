using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] InputAction _normalAttack;
    
    private float _currentFireRate;
    private float _nextFire = -1;
    private Transform _currentFiringPosition;
    private int _currentBulletID;
    private Weapon[] _weaponArray;
    private Weapon _equipedWeapon;

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
        _weaponArray = GetComponentsInChildren<Weapon>();
        foreach(var weapon in _weaponArray)
        {
            if(weapon.IsEquiped)
            {
                EquipWeapon(weapon);
            }
        }
    }

    void Update()
    {
        if(_normalAttack.ReadValue<float>() > 0.5f && Time.time > _nextFire)
        {
            var bullet = PoolManager.Instance.RequestObject(_currentBulletID);
            bullet.transform.position = _currentFiringPosition.position;
            bullet.GetComponent<Bullet>().SetBulletDirection(transform.right);
            _nextFire = Time.time + _currentFireRate;
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        _equipedWeapon = weapon;
        _currentFireRate = weapon.FireRate;
        _currentFiringPosition = weapon.FiringPosition;
        _currentBulletID = weapon.GetBulletID();
    }
}
