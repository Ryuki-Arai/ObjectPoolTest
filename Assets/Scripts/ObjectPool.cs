using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] 
    List<ObjectPoolParam> _poolObj = default;

    [SerializeField] 
    Transform _poolParent;

    List<ObjectParam> _pools = new List<ObjectParam>();

    private void InstantiatePool()
    {
        foreach(var obj in _poolObj)
        {
            for(int i = 0; i < _poolObj.Count; i++)
            {
                var prefab = Instantiate(obj.Obj, _poolParent);
                prefab.name = obj.Name;
                prefab.SetActive(false);
                _pools.Add(new ObjectParam(prefab.name, prefab));
            }
        }
    }

    public GameObject UseObject(string name, Vector3 pos)
    {
        if (name is null) return null;

        foreach(var pool in _pools)
        {
            if(!pool.gameObject.activeSelf && pool.name == name)
            {
                pool.gameObject.transform.position = pos;
                pool.gameObject.SetActive(true);
                return pool.gameObject;
            }
        }
    }

}

[Serializable]
public class ObjectParam
{
    public string name { get; }

    public GameObject gameObject { get; }

    public ObjectParam(string name, GameObject obj)
    {
        this.name = name;
        this.gameObject = obj;
    }
}

[Serializable]
public class ObjectPoolParam
{
    [SerializeField] 
    private string name;
    
    [SerializeField] 
    private GameObject obj;
    
    [SerializeField,Min(0)] 
    private int maxCount;

    public string Name => name;

    public GameObject Obj => obj;

    public int MaxCount => maxCount;

}
