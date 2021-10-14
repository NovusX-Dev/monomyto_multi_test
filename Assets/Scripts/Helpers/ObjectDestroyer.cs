using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    [SerializeField] float _time = 5f;

    void Start()
    {
        Destroy(gameObject, _time);
    }

}
