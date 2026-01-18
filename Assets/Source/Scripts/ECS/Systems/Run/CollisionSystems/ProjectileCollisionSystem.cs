using ECS.Components;
using Leopotam.Ecs;

namespace Systems
{
    public class ProjectileCollisionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CollisionEnterComponent, ProjectileComponent> _filter;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                entity.Get<DestructionEventComponent>();
            }
        }
    }
}