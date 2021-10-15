using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem
{
    public string name;
    public GameObject objectPrefab;
    public int objectCount;
    public bool _shouldExpand;
}

public class PoolManager : Singleton<PoolManager>
{
    [SerializeField] Transform _poolContainer;
    [SerializeField] List<ObjectPoolItem> _objectsToPool;

    private List<GameObject> _objectPool;

    void Start()
    {
        _objectPool = new List<GameObject>();
        foreach (var item in _objectsToPool)
        {
            for(int i = 0; i < item.objectCount; i++)
            {
                GameObject obj = Instantiate(item.objectPrefab);
                obj.transform.parent = _poolContainer;
                obj.SetActive(false);
                _objectPool.Add(obj);
            }
        }
    }


    public GameObject RequestObject(string tag)
    {

        foreach(var obj in _objectPool)
        {
            if(!obj.activeInHierarchy && obj.tag == tag)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        foreach (var item in _objectsToPool)
        {
            for (int i = 0; i < item.objectCount; i++)
            {
                GameObject newObj = Instantiate(item.objectPrefab);
                newObj.transform.parent = _poolContainer;
                _objectPool.Add(newObj);
                return newObj;
            }
        }

        return null;
        
    }
}
