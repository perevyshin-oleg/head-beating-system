using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PunchFeature.Infrastructure.ObjectPools
{
    public class ObjectPool<T> : IObjectPool<T> where T: Component
    {
        private T _prefab;
        private List<T> _objects = new();
        private readonly Transform _parent;

        public ObjectPool(T prefab, int initialCount)
        {
            _prefab = prefab;
            
            _parent = new GameObject("ParticlesObjectPool").transform;
            
            for (int i = 0; i < initialCount; i++)
                Create();
        }

        public T Get()
        {
            var obj = _objects.FirstOrDefault(x => x.gameObject.activeSelf == false);

            if (obj == null)
            {
                obj = Create();
            }
            
            obj.gameObject.SetActive(true);
            return obj;
        }

        public T Create()
        {
            T obj = Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _objects.Add(obj);
            return obj;
        }

        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
        }
    }
}
