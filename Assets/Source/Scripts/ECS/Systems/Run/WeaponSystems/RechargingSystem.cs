using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems
{
    public class RechargingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RechargeComponent> _filter;
        private EcsWorld _ecsWorld;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var rechargeComponent = ref _filter.Get1(index);

                rechargeComponent.timeLeft += Time.deltaTime;

                if(rechargeComponent.timeLeft >= rechargeComponent.rechargeDuration)
                    _filter.GetEntity(index).Del<RechargeComponent>();
            }
        }
    }
}