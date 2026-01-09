using ECS.Components.Input;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class RotationInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RotationInputEventComponent> _inputEventsFilter;

        public void Run()
        {
            var mousePosition = Input.mousePosition;
            var horizontal = (mousePosition.x / Screen.width) * 2 - 1;
            var vertical = (mousePosition.y / Screen.height) * 2 - 1;

            foreach (var input in _inputEventsFilter)
            {
                ref var inputEvent = ref _inputEventsFilter.Get1(input);
                inputEvent.direction = new Vector3(horizontal, 0f, vertical);
            }
        }
    }
}