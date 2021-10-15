using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] InputAction _normalAttack, _reaload;
    
    private float _currentFireRate;
    private float _nextFire = -1;
    private Weapon[] _weaponArray;
    private Weapon _equipedWeapon;

    private void OnEnable()
    {
        _normalAttack.Enable();
        _reaload.Enable();
    }

    private void OnDisable()
    {
        _normalAttack.Disable();
        _reaload.Disable();
    }

    void Start()
    {
        _weaponArray = GetComponentsInChildren<Weapon>(true);
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
        foreach (var weapon in _weaponArray)
        {
            if (weapon.IsEquiped)
            {
                EquipWeapon(weapon);
            }
        }

        if (_normalAttack.ReadValue<float>() > 0.5f && Time.time > _nextFire)
        {
            _equipedWeapon.FireWeapon();
            _nextFire = Time.time + _currentFireRate;
        }

        if(_reaload.ReadValue<float>() > 0.5f)
        {
            _equipedWeapon.ReloadWeapon();
        }
    }

    public void EquipWeapon(Weapon weapon)
    {
        _equipedWeapon = weapon;
        _currentFireRate = weapon.FireRate;
    }
}
