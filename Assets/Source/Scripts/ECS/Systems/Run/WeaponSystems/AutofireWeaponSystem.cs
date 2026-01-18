using ECS.Components;
using Leopotam.Ecs;

namespace ECS.Systems
{
    public class AutofireWeaponSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TurretComponent> _filter;

        public void Run()
        {
            foreach (var index in _filter)
            {
                ref var turretComponent = ref _filter.Get1(index);

                foreach (var entity in turretComponent.weapons)
                {
                    if (entity.Has<RechargeComponent>())
                        continue;

                    entity.Get<ShotComponent>();
                }
            }
        }
    }
}