using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class ProjectileMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovableComponent, ProjectileComponent> _projectileMoveFilter;

        public void Run()
        {
            foreach (var index in _projectileMoveFilter)
            {
                ref var movableComponent = ref _projectileMoveFilter.Get1(index);

                var translation = Vector3.forward * Time.deltaTime * movableComponent.moveSpeed;

                movableComponent.transform.Translate(translation, Space.Self);

                movableComponent.isMoving = translation.sqrMagnitude > 0;
            }
        }
    }
}