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
    private bool _isReloading = false;
    private WaitForSeconds _reloadWait;

    public Transform FiringPosition => _firingPosition;
    public float FireRate => _weaponSO.fireRate;
    public bool IsEquiped => _isEquiped;

    private void OnEnable()
    {
        _isEquiped = gameObject.activeInHierarchy;
    }

    private void Start()
    {
        if(_weaponSO == null) Debug.LogError("Weapon Scriptable Object is not assigned!");
        _currentAmmoCount = _weaponSO.maxAmmoCount;
        _reloadWait = new WaitForSeconds(_weaponSO.reloadTime);
    }

    private void Update()
    {
        _isEquiped = gameObject.activeInHierarchy;
    }

    public void FireWeapon()
    {
        if(!_canShoot) return;

        var bullet = PoolManager.Instance.RequestObject(GetBulletID());
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

    public void ReloadWeapon()
    {
        if(!_isReloading)
            StartCoroutine(ReloadWeaponRoutine());
    }

    IEnumerator ReloadWeaponRoutine()
    {
        _isReloading = true;
        _canShoot = false;
        yield return _reloadWait;
        _currentAmmoCount = _weaponSO.maxAmmoCount;
        _canShoot = true;
        _isReloading = false;
    }

    private int GetBulletID()
    {
        return _weaponSO.bulletPrefab.GetComponent<Bullet>().GetBulletID();
    }
}
