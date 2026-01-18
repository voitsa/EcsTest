using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class HealthDamageSystem : IEcsRunSystem
    {
        private EcsFilter<HealthComponent, DamageReceiveComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var healthComponent = ref _filter.Get1(index);
                ref var damageReceiveComponent = ref _filter.Get2(index);
                var entity = _filter.GetEntity(index);

                healthComponent.currentValue -= damageReceiveComponent.value;
                healthComponent.currentValue = Mathf.Clamp(healthComponent.currentValue, healthComponent.minValue, healthComponent.maxValue);

                if (healthComponent.currentValue <= healthComponent.minValue)
                {
                    entity.Get<DestructionEventComponent>();
                    entity.Del<FollowComponent>();
                    entity.Del<TargetableComponent>();
                    continue;
                }

                entity.Del<DamageReceiveComponent>();
            }
        }
    }
}