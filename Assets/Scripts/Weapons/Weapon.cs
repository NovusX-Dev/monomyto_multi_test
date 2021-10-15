using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] Transform _firingPosition;
    [SerializeField] int _maxAmmoCount = 15;
    [SerializeField] float _fireRate = 0.25f;

    private bool _isEquiped;

    
    public Transform FiringPosition => _firingPosition;
    public float FireRate => _fireRate;
    public bool IsEquiped => _isEquiped;

    private void OnEnable()
    {
        _isEquiped = gameObject.activeInHierarchy;
    }

    private void Update()
    {
        _isEquiped = gameObject.activeInHierarchy;
    }

    public int GetBulletID()
    {
        return _bulletPrefab.GetComponent<Bullet>().GetBulletID();
    }
}
