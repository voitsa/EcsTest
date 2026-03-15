using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CameraComponent, TargetableComponent, PlayerTagComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var cameraComponent = ref _filter.Get1(index);
                ref var targetableComponent = ref _filter.Get2(index);

                var targetPosition = targetableComponent.transform.position;
                var cameraPositionY = cameraComponent.camera.transform.position.y;
                cameraComponent.camera.transform.position = new Vector3(targetPosition.x, cameraPositionY, targetPosition.z);
            }
        }
    }
}