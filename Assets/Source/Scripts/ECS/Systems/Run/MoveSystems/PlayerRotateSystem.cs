using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class PlayerRotateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RotatableComponent, RotationInputEventComponent> _playerMoveFilter;

        public void Run()
        {
            foreach (var index in _playerMoveFilter)
            {
                ref var rotatableComponent = ref _playerMoveFilter.Get1(index);
                ref var inputComponent = ref _playerMoveFilter.Get2(index);

                rotatableComponent.transform.rotation = Quaternion.LookRotation(inputComponent.direction);;
            }
        }
    }
}