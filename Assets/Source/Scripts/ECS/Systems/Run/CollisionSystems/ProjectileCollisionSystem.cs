using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class ProjectileCollisionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CollisionEnterComponent, ProjectileComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var entity = ref _filter.GetEntity(index);
                entity.Get<DestructionEventComponent>();
            }
        }
    }
}