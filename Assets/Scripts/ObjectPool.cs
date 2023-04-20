using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameTool.ObjectPool
{
    public class ObjectPool : SingletonMonoBehaviour<ObjectPool>
    {
        [SerializeField]
        List<ObjectPoolParam> _poolObj = default;

        [SerializeField]
        Transform _poolParent;

        List<ObjectParam> _pools = new List<ObjectParam>();

        protected override void Awake()
        {
            base.Awake();
            InstantiatePool();
        }

        private void InstantiatePool()
        {
            foreach (var obj in _poolObj)
            {
                for (int i = 0; i < obj.MaxCount; i++)
                {
                    var prefab = Instantiate(obj.GameObject, _poolParent);
                    prefab.name = obj.Name;
                    prefab.SetActive(false);
                    _pools.Add(new ObjectParam(prefab.name, prefab));
                }
            }
        }

        public GameObject UseObject(string name, Vector3 pos)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Debug.LogError($"名前を指定してください");
                return null;
            }

            foreach (var pool in _pools)
            {
                if (!pool.GameObject.activeSelf && pool.Name == name)
                {
                    pool.GameObject.transform.position = pos;
                    pool.GameObject.SetActive(true);
                    return pool.GameObject;
                }
            }

            var searchObj = _pools.Find(x => x.Name == name);

            if (searchObj is null)
            {
                Debug.LogError($"オブジェクト:{name}が存在しません");
                return null;
            }

            var prefab = Instantiate(searchObj.GameObject, _poolParent);
            prefab.name = searchObj.Name;
            prefab.transform.position = pos;
            prefab.SetActive(true);
            _pools.Add(new ObjectParam(prefab.name, prefab));

            return prefab;
        }

        public T UseObject<T>(string name, Vector3 pos) where T : Component
        {
            if (UseObject(name, pos).TryGetComponent(out T component))
            {
                return component;
            }
            else
            {
                Debug.LogError($"オブジェクト:{name}にコンポーネント:{component}が存在しません");
                return null;
            }
        }

    }

    [Serializable]
    public class ObjectParam
    {
        public string Name { get; private set; }

        public GameObject GameObject { get; private set; }

        public ObjectParam(string name, GameObject gameObject)
        {
            Name = name;
            GameObject = gameObject;
        }
    }

    [Serializable]
    public class ObjectPoolParam
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private GameObject gameObject;

        [SerializeField, Min(0)]
        private int maxCount;

        public string Name => name;

        public GameObject GameObject => gameObject;

        public int MaxCount => maxCount;

    }

}