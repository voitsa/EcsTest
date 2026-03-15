using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class ShootingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<WeaponComponent, ShotComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var weaponComponent = ref _filter.Get1(index);

                weaponComponent.projectileBuilder.Build(weaponComponent);

                ref var entity = ref _filter.GetEntity(index);
                entity.Del<ShotComponent>();

                ref var rechargeComponent = ref entity.Get<RechargeComponent>();
                rechargeComponent.rechargeDuration = weaponComponent.shotDelay;
            }
        }
    }
}