using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] Transform _poolContainer;
    [SerializeField] GameObject _objectPrefab;
    [SerializeField] int _poolCount;

    private List<GameObject> _objectPool;

    void Start()
    {
        _objectPool = GeneratePool(_poolCount);
    }

    List<GameObject> GeneratePool(int amount)
    {
        _objectPool = new List<GameObject>();

        for(int i = 0; i < amount; i++)
        {
            Debug.Log("0");
            GameObject obj = Instantiate(_objectPrefab);
            Debug.Log("1");
            obj.transform.parent = _poolContainer;
            Debug.Log("2");
            obj.SetActive(false);
            Debug.Log("3");
            _objectPool.Add(obj);
            Debug.Log("4");
        }

        return _objectPool;
    }

    public GameObject RequestObject()
    {
        foreach(var obj in _objectPool)
        {
            if(!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        var newObj = Instantiate(_objectPrefab);
        newObj.transform.parent = _poolContainer;
        _objectPool.Add(newObj);

        return newObj;
    }
}
