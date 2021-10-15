using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] InputAction _normalAttack;
    
    private float _currentFireRate;
    private float _nextFire = -1;
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
        _weaponArray = GetComponentsInChildren<Weapon>(true);
        foreach(var weapon in _weaponArray)
        {
            if(weapon.IsEquiped)
            {
                _equipedWeapon = weapon;
                _currentFireRate = weapon.FireRate; ;
            }
        }
    }

    void Update()
    {
        if (_normalAttack.ReadValue<float>() > 0.5f && Time.time > _nextFire)
        {
            _equipedWeapon.FireWeapon();
            _nextFire = Time.time + _currentFireRate;
        }
    }
    public void EquipWeapon(Weapon weapon)
    {
        _equipedWeapon.gameObject.SetActive(false);
        _equipedWeapon = weapon;
        _equipedWeapon.gameObject.SetActive(true);
        _currentFireRate = weapon.FireRate;
        weapon.AddAmmo();
    }
}
