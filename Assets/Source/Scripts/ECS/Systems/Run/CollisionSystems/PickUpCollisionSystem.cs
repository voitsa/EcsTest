using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class PickUpCollisionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PickUpTagComponent, CollisionEnterComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var collisionComponent = ref _filter.Get2(index);

                var otherEntity = collisionComponent.other;

                if (otherEntity.IsAlive() && otherEntity.Has<PlayerTagComponent>())
                {
                    ref var entity = ref _filter.GetEntity(index);
                    entity.Get<DestructionEventComponent>();
                    entity.Del<CollisionEnterComponent>();
                }
            }
        }
    }
}