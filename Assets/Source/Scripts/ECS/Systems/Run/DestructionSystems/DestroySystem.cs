using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class DestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<DestructionComponent, DestructionEventComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var objectDestructionComponent = ref _filter.Get1(index);

                Object.Destroy(objectDestructionComponent.destroyObject);
                _filter.GetEntity(index).Destroy();
            }
        }
    }
}