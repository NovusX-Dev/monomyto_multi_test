using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon", order = 1)]
public class WeaponSO : ScriptableObject
{
    public GameObject bulletPrefab;
    public int ammotPickUpAmount = 5;
    public float fireRate = 0.25f;
    public float reloadTime = 0.5f;
    public int weaponID;
}
