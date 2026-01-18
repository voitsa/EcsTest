using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class CollisionDamageSystem : IEcsRunSystem
    {
        private EcsFilter<DamageInflictComponent, CollisionEnterComponent, CollisionDamageAllowComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var damageInflictComponent = ref _filter.Get1(index);
                ref var collisionComponent = ref _filter.Get2(index);

                var otherEntity = collisionComponent.other;

                if (otherEntity.IsAlive() && otherEntity.Has<HealthComponent>())
                {
                    ref var damageReceiveComponent = ref otherEntity.Get<DamageReceiveComponent>();
                    damageReceiveComponent.value = damageInflictComponent.value;
                }
            }
        }
    }
}