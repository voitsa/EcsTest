using ECS.Components;
using ECS.Components.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class ProjectileMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovableComponent, ProjectileComponent> _projectileMoveFilter;

        public void Run()
        {
            foreach (var entity in _projectileMoveFilter)
            {
                ref var movableComponent = ref _projectileMoveFilter.Get1(entity);

                var translation = Vector3.forward * Time.deltaTime * movableComponent.moveSpeed;

                movableComponent.transform.Translate(translation, Space.Self);

                movableComponent.isMoving = translation.sqrMagnitude > 0;
            }
        }
    }
}