using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class RotationInputSystem : IEcsRunSystem
    {
        private const float MinScreenNdc = -1f;
        private const float MaxScreenNdc = 1f;

        private readonly EcsFilter<RotationInputEventComponent> _filter;

        public void Run()
        {
            var mousePosition = Input.mousePosition;
            var normalizedX = mousePosition.x / Screen.width;
            var normalizedY = mousePosition.y / Screen.height;
            var horizontal = Mathf.Lerp(MinScreenNdc, MaxScreenNdc, normalizedX);
            var vertical = Mathf.Lerp(MinScreenNdc, MaxScreenNdc, normalizedY);

            foreach (var index in _filter)
            {
                ref var inputEventComponent = ref _filter.Get1(index);
                inputEventComponent.direction = new Vector3(horizontal, 0f, vertical);
            }
        }
    }
}