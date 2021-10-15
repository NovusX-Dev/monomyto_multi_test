using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon _weaponPickup;
    [SerializeField] int _pickupID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerAttacker>().EquipWeapon(_weaponPickup, _pickupID);
            Destroy(gameObject);
        }
    }

}
