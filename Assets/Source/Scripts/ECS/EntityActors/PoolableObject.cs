using UnityEngine;
using Utility;

namespace ECS.EntityActors
{
    public class PoolableObject : MonoBehaviour, IPoolable
    {
        private System.Action<PoolableObject> _returnAction;

        public void Initialize(System.Action<PoolableObject> returnAction)
        {
            _returnAction = returnAction;
        }

        public void ReturnToPool()
        {
            _returnAction?.Invoke(this);
        }
    }
}