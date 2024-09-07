using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Stack<GameObject> _objects = new Stack<GameObject>();
    private GameObject _prefab;

    public ObjectPool(GameObject prefab)
    {
        _prefab = prefab;
    }

    public GameObject GetObject()
    {
        if (_objects.TryPop(out GameObject o))
        {
            o.SetActive(true);
            return o;
        }

        return Object.Instantiate(_prefab);
    }

    public void AddObject(GameObject obj)
    {
        obj.SetActive(false);
        _objects.Push(obj);
    }
}
