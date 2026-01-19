using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class MoveInputSystem : IEcsRunSystem
    {
        private const KeyCode UpKey = KeyCode.W;
        private const KeyCode DownKey = KeyCode.S;
        private const KeyCode LeftKey = KeyCode.A;
        private const KeyCode RightKey = KeyCode.D;

        private readonly EcsFilter<MoveInputEventComponent> _filter;

        public void Run()
        {
            Vector2 direction = Vector2.zero;

            if(Input.GetKey(UpKey))
                direction += Vector2.up;

            if(Input.GetKey(DownKey))
                direction += Vector2.down;

            if(Input.GetKey(LeftKey))
                direction += Vector2.left;

            if(Input.GetKey(RightKey))
                direction += Vector2.right;

            direction = direction.normalized;

            foreach (var index in _filter)
            {
                ref var moveInputEventComponent = ref _filter.Get1(index);
                moveInputEventComponent.direction = new Vector3(direction.x, 0f, direction.y);
            }
        }
    }
}