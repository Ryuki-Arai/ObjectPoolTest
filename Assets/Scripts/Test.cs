using GameTool.ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] string _name;

    public void OnClick()
    {
        ObjectPool.Instance.UseObject(_name,Vector3.zero);
    }
}
