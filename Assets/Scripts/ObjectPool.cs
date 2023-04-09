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

    }

    public GameObject UseObject()
    {

    }

}

[Serializable]
public class ObjectParam
{
    public string name { get; set; }

    public GameObject obj { get; set; }
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
