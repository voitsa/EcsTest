using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.MonoBehaviours
{
    public class ColliderObserver : MonoBehaviour
    {
        private EcsWorld _ecsWorld;

        public void Initialize(EcsWorld ecsWorld, EcsEntity ecsEntity)
        {
            _ecsWorld = ecsWorld;
            EcsEntity = ecsEntity;
        }

        public EcsEntity EcsEntity { get; private set; }

        private void OnCollisionEnter(Collision other)
        {
            if (_ecsWorld == null && !EcsEntity.IsAlive())
                return;

            var otherObserver = other.gameObject.GetComponent<ColliderObserver>();
            Vector3 collisionPoint = other.GetContact(0).point;

            if (otherObserver != null)
            {
                EcsEntity.Get<CollisionEnterComponent>() = new CollisionEnterComponent
                {
                    other = otherObserver.EcsEntity,
                    collisionPoint = collisionPoint
                };
            }
        }
    }
}