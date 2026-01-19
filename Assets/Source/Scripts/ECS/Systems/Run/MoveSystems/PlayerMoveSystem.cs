using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovableComponent, MoveInputEventComponent> _playerMoveFilter;

        public void Run()
        {
            foreach (var index in _playerMoveFilter)
            {
                ref var movableComponent = ref _playerMoveFilter.Get1(index);
                ref var inputComponent = ref _playerMoveFilter.Get2(index);

                movableComponent.transform.Translate(inputComponent.direction *
                                                     Time.deltaTime * movableComponent.moveSpeed, Space.Self);
                movableComponent.isMoving = inputComponent.direction.sqrMagnitude > 0;
            }
        }
    }
}