using ECS.Components;
using ECS.Components.Movement;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class CameraDistanceSystem : IEcsRunSystem
    {
        private EcsFilter<CameraComponent, MovableComponent, PlayerTagComponent> _filter = null;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var cameraComponent = ref _filter.Get1(index);
                ref var movableComponent = ref _filter.Get2(index);

                Vector3 newPosition = cameraComponent.defaultPosition;
                newPosition.y += newPosition.y * movableComponent.moveSpeed * cameraComponent.distanceRate;
                cameraComponent.camera.transform.position = newPosition;
            }
        }
    }
}