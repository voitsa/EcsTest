using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;
using Utility;

namespace ECS.Systems
{
    public class RandomTurretRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RotatableComponent, FollowComponent, TurretComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var followComponent = ref _filter.Get2(index);

                if (followComponent.target != null)
                    continue;

                ref var rotatableComponent = ref _filter.Get1(index);
                var direction = Vector3.zero;
                direction = direction.RandomNormalized(zeroY: true);

                if (rotatableComponent.transform == null)
                    continue;

                rotatableComponent.transform.forward = direction;
            }
        }
    }
}