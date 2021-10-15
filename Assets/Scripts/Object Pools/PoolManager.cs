using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    [Tooltip("Must be equal to a butt ID")] [SerializeField] int _poolID;
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
            GameObject obj = Instantiate(_objectPrefab);
            obj.transform.parent = _poolContainer;
            obj.SetActive(false);
            _objectPool.Add(obj);
        }

        return _objectPool;
    }

    public GameObject RequestObject(int iD)
    {
        if(iD != _poolID) 
        { 
            Debug.LogError("IDs do not match");
            return null;
        } 

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
