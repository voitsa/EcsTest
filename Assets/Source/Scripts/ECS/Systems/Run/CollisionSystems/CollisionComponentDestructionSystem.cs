using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class CollisionComponentDestructionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CollisionEnterComponent, CollisionDestructionComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                _filter.GetEntity(index).Del<CollisionEnterComponent>();
            }
        }
    }
}