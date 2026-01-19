using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolable
    {
        private readonly Queue<T> _objects = new();
        private readonly T _prefab;
        private readonly Transform _parent;

        public ObjectPool(T prefab, int initialCount = 1, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;

            for (int i = 0; i < initialCount; i++)
                CreateObject();
        }

        private T CreateObject()
        {
            var obj = Object.Instantiate(_prefab, _parent);
            obj.gameObject.SetActive(false);
            _objects.Enqueue(obj);
            return obj;
        }

        public T Get(Vector3 position, Quaternion rotation)
        {
            var obj = _objects.Count > 0 ? _objects.Dequeue() : CreateObject();
            obj.gameObject.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            return obj;
        }

        public void ReturnToPool(T obj)
        {
            obj.gameObject.SetActive(false);
            _objects.Enqueue(obj);
        }
    }
}