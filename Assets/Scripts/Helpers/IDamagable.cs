using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable<T>
{
    public void Damage(T damageTaken);
}
