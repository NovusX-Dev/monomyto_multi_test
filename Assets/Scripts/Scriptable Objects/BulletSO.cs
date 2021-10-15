using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullet", order = 2)]
public class BulletSO : ScriptableObject
{
    public string _tag;
    public float bulletSpeed = 2f;
    public float bulletPower = 0.5f;
    public Sprite bulletSprite = null;

    public void SetTagString(string tag)
    {
        _tag = tag;
    }
}
