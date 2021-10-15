using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullet", order = 2)]
public class BulletSO : ScriptableObject
{
    public float bulletSpeed = 2f;
    public float bulletPower = 0.5f;
    [Tooltip("Must be equal to a pool manager ID")] public int bulletID;
    public Sprite bulletSprite = null;

}
