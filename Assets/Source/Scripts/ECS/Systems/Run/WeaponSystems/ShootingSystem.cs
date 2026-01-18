using ECS.Components;
using ECS.MonoBehaviours;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class ShootingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<WeaponComponent, ShotComponent> _filter;

        public ShootingSystem(EcsWorld ecsWorld)
        {
        }

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var weapon = ref _filter.Get1(index);

                weapon.projectileBuilder.Build(weapon);

                ref var entity = ref _filter.GetEntity(index);
                entity.Del<ShotComponent>();

                ref var recharge = ref entity.Get<RechargeComponent>();
                recharge.rechargeDuration = weapon.shotDelay;
            }
        }
    }
}