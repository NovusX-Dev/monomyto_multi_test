using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform _firingPosition;
    [SerializeField] WeaponSO _weaponSO;

    private bool _isEquiped;
    private int _currentAmmoCount;
    private bool _canShoot = true;

    public Transform FiringPosition => _firingPosition;
    public float FireRate => _weaponSO.fireRate;
    public bool IsEquiped => _isEquiped;


    Bullet _bullet;

    private void OnEnable()
    {
        _isEquiped = gameObject.activeInHierarchy;
    }

    private void Start()
    {
        if(_weaponSO == null) Debug.LogError("Weapon Scriptable Object is not assigned!");
        AddAmmo();
        _bullet = _weaponSO.bulletPrefab.GetComponent<Bullet>();
    }

    private void Update()
    {
        _isEquiped = gameObject.activeInHierarchy;
    }

    public void FireWeapon()
    {
        if(!_canShoot) return;

        var bullet = PoolManager.Instance.RequestObject(GetBulletTag());
        bullet.transform.position = _firingPosition.position;
        bullet.GetComponent<Bullet>().SetBulletDirection(transform.right);

        UseAmmo();
    }

    private void UseAmmo()
    {
        _currentAmmoCount --;

        if(_currentAmmoCount <= 0)
        {
            Debug.Log("NO AMMMMOOOOOO!");
            _currentAmmoCount = 0;
            _canShoot = false;
        }
    }

    public void AddAmmo()
    {
        _currentAmmoCount += _weaponSO.ammotPickUpAmount;
    }

    private string GetBulletTag()
    {
        return _bullet.GetBulletTag();
    }

    public int GetWeaponID()
    {
        return _weaponSO.weaponID;
    }

    public int GetAmmoCount()
    {
        return _currentAmmoCount;
    }

    public float GetBulletPower()
    {
        return _bullet.GetBulletPower();
    }

    public float SetBulletPower(float power)
    {
        return _bullet.SetBulletPower(power);
    }
}
