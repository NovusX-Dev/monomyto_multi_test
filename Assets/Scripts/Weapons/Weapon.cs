using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Transform _firingPosition;
    [SerializeField] WeaponSO _weaponSO;

    private bool _isEquiped;

    
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
    }

    private void Update()
    {
        _isEquiped = gameObject.activeInHierarchy;
    }

    public int GetBulletID()
    {
        return _weaponSO.bulletPrefab.GetComponent<Bullet>().GetBulletID();
    }
}
