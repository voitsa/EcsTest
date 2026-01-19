using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class RotationInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RotationInputEventComponent> _filter;

        public void Run()
        {
            var mousePosition = Input.mousePosition;
            var horizontal = (mousePosition.x / Screen.width) * 2 - 1;
            var vertical = (mousePosition.y / Screen.height) * 2 - 1;

            foreach (var index in _filter)
            {
                ref var inputEventComponent = ref _filter.Get1(index);
                inputEventComponent.direction = new Vector3(horizontal, 0f, vertical);
            }
        }
    }
}