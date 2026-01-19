using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class HealthRefillSystem : IEcsRunSystem
    {
        private EcsFilter<HealthComponent, HealthReceiveComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var healthComponent = ref _filter.Get1(index);
                ref var healthReceiveComponent = ref _filter.Get2(index);
                var entity = _filter.GetEntity(index);

                if (healthComponent.currentValue <= healthComponent.minValue)
                {
                    entity.Del<FollowComponent>();
                    entity.Del<TargetableComponent>();
                    continue;
                }

                healthComponent.currentValue += healthReceiveComponent.receiveAmount;
                healthComponent.currentValue = Mathf.Clamp(healthComponent.currentValue, healthComponent.minValue, healthComponent.maxValue);

                entity.Del<DamageReceiveComponent>();
            }
        }
    }
}