using ECS.Components;
using ECS.Components.Detection;
using ECS.Components.Movement;
using Leopotam.Ecs;
using UnityEngine;
using Utility;

namespace Systems
{
    public class RandomTurretRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RotatableComponent, FollowComponent, TurretComponent> _filter;

        public void Run()
        {
            foreach (var entity in _filter)
            {
                ref var followComponent = ref _filter.Get2(entity);

                if (followComponent.target != null)
                {
                    continue;
                }

                ref var rotatableComponent = ref _filter.Get1(entity);
                var direction = Vector3.zero;
                direction = direction.RandomNormalized(zeroY: true);
                rotatableComponent.transform.forward = direction;
            }
        }
    }
}