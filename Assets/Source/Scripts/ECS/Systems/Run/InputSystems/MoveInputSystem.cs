using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class MoveInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MoveInputEventComponent> _filter;

        public void Run()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            var direction = new Vector2(horizontal, vertical);

            if (direction.sqrMagnitude > 1f)
                direction.Normalize();

            foreach (var index in _filter)
            {
                ref var moveInputEventComponent = ref _filter.Get1(index);
                moveInputEventComponent.direction = new Vector3(direction.x, 0f, direction.y);
            }
        }
    }
}