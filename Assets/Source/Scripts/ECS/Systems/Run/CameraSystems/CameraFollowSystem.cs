using ECS.Components;
using ECS.Components.Detection;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class CameraFollowSystem : IEcsRunSystem
    {

        private EcsFilter<CameraComponent, TargetableComponent, PlayerTagComponent> _filter = null;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var camera = ref _filter.Get1(index);
                ref var target = ref _filter.Get2(index);

                var targetPosition = target.transform.position;
                var cameraPositionY = camera.camera.transform.position.y;
                camera.camera.transform.position = new Vector3(targetPosition.x, cameraPositionY, targetPosition.z);
            }
        }
    }
}