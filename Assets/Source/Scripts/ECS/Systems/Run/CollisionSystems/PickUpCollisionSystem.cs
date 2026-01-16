using ECS.Components;
using Leopotam.Ecs;

namespace Systems
{
    public class PickUpCollisionSystem : IEcsRunSystem
    {
        private EcsFilter<PickUpTagComponent, CollisionEnterComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var pickUpTagComponent = ref _filter.Get1(index);
                ref var collisionComponent = ref _filter.Get2(index);

                var otherEntity = collisionComponent.other;

                if (otherEntity.IsAlive() && otherEntity.Has<PlayerTagComponent>())
                {
                    ref var entity = ref _filter.GetEntity(index);
                    ref var destructionComponent = ref entity.Get<DestructionComponent>();
                    destructionComponent.destroyObject = pickUpTagComponent.pickUp;
                }
            }
        }
    }
}